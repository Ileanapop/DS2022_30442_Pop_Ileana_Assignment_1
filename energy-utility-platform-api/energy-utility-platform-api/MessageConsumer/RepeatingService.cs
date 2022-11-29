using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Services;
using energy_utility_platform_api.Utils.CustomExceptions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.WebSockets;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace energy_utility_platform_api.MessageConsumer
{
    public class RepeatingService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(1000));

        private  Dictionary<Guid, float> energyValues = new Dictionary<Guid, float>();
        private  Dictionary<Guid, DateTime> lastReadings = new Dictionary<Guid, DateTime>();
        private readonly IServiceProvider _serviceProvider;

        public RepeatingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("reading message..");
                await ReadMessage();
            }
        }

        private async Task ReadMessage()
        {
            try{
                var factory = new ConnectionFactory { 
                    HostName = "rabbitmq",
                    VirtualHost = "/",
                    UserName = "guest",
                    Password = "guest"
                };
                var connection = factory.CreateConnection();

                using
                var channel = connection.CreateModel();
                channel.QueueDeclare("readings", exclusive: false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                    var energyConsumptionDTO = CreateDto(message);

                    Console.WriteLine("---------------");
                    Console.WriteLine(energyConsumptionDTO.DateTime);
                    Console.WriteLine("---------------");


                    //check last reading
                    DateTime lastReadingForDevice = lastReadings.GetValueOrDefault(energyConsumptionDTO.UserDeviceId);

                    Console.WriteLine("What is in map");
                    Console.WriteLine(lastReadingForDevice);

                    if (lastReadingForDevice != default)
                    {              

                        if (lastReadingForDevice.Minute != energyConsumptionDTO.DateTime.Minute)
                        {

                            
                            Console.WriteLine("Store to db minute changed");

                            //store to db minute energy consumtion
                            EnergyConsumptionDtoForCreate newConsumption = new EnergyConsumptionDtoForCreate
                            {
                                UserDeviceId = energyConsumptionDTO.UserDeviceId,
                                DateTime = lastReadingForDevice,
                                Consumption = this.energyValues.GetValueOrDefault(energyConsumptionDTO.UserDeviceId)
                            };


                            using (IServiceScope scope = _serviceProvider.CreateScope())
                            {

                                IEnergyConsumptionService energyConsumptionService =
                                    scope.ServiceProvider.GetRequiredService<IEnergyConsumptionService>();

                                IUserDeviceRepository userDeviceRepository =
                                    scope.ServiceProvider.GetRequiredService<IUserDeviceRepository>();

                                try
                                {
                                    var result = await energyConsumptionService.Add(newConsumption);
                                    Console.WriteLine("Energy consumption {0} inserted into db...", result.Id);


                                    //notify client
                                    var engDevice = await userDeviceRepository.GetUserDeviceById(energyConsumptionDTO.UserDeviceId);

                                    if(newConsumption.Consumption > engDevice.EnergyDevice.MaxHourlyEnergy)
                                    {
                                        Console.WriteLine("hereeee");
                                        Console.WriteLine(engDevice.UserId.ToString());

                                        var _connectedClientsRepository = ConnectedClientsRepository.GetInstance();

                                        var connectedClientSocket = _connectedClientsRepository.GetClientSocket(engDevice.UserId.ToString());

                                        //send data to frontend
                                        Console.WriteLine("in rpeatingggg");
                                        Console.WriteLine(connectedClientSocket);

                                        string warning = "Notification: consumption for device " + engDevice.EnergyDevice.Id.ToString() + " " + engDevice.EnergyDevice.ModelName + " exceeded: max: " + engDevice.EnergyDevice.MaxHourlyEnergy.ToString() + " registered " + newConsumption.Consumption.ToString();

                                        var serverMsg = Encoding.UTF8.GetBytes(warning);
                                        Console.WriteLine(connectedClientSocket.State);
                                        await connectedClientSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), 0, true, CancellationToken.None);
                                    }

                                }
                                catch (NotFoundException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }           

                            //reinitialize minute energy consumption in map
                            this.energyValues[energyConsumptionDTO.UserDeviceId] = energyConsumptionDTO.Consumption;
                            lastReadings[energyConsumptionDTO.UserDeviceId] = energyConsumptionDTO.DateTime;

                        }
                        else
                        {
                            //add consymption 
                            Console.WriteLine("---Before:");
                            Console.WriteLine(this.energyValues[energyConsumptionDTO.UserDeviceId]);
                            this.energyValues[energyConsumptionDTO.UserDeviceId] += energyConsumptionDTO.Consumption;
                            Console.WriteLine("---After:");
                            Console.WriteLine(this.energyValues[energyConsumptionDTO.UserDeviceId]);
                            //save last reading time
                            this.lastReadings[energyConsumptionDTO.UserDeviceId] = energyConsumptionDTO.DateTime;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Initializing entry in map");

                        //add consymption 
                        this.energyValues[energyConsumptionDTO.UserDeviceId] = energyConsumptionDTO.Consumption;

                        //save last reading time
                        this.lastReadings[energyConsumptionDTO.UserDeviceId] = energyConsumptionDTO.DateTime;

                        Console.WriteLine(energyValues[energyConsumptionDTO.UserDeviceId]);
                        Console.WriteLine(lastReadings[energyConsumptionDTO.UserDeviceId]);
                        Console.WriteLine("Initializing ready");

                    }

                };

                channel.BasicConsume(queue: "readings", autoAck: true, consumer: consumer);
            }
            catch(Exception){
                Console.WriteLine("No message received yet");
            }

        }

        private static EnergyConsumptionDtoForCreate CreateDto(string body)
        {

            //ReadingDTO readingDto = (ReadingDTO)JsonConvert.DeserializeObject(body);
            //readingDto As ReadingDTO = JsonConvert.DeserializeObject(Of ReadingDTO)(body);
            ReadingDTO readingDto = JsonConvert.DeserializeObject<ReadingDTO>(body);

            return new EnergyConsumptionDtoForCreate {
                DateTime = readingDto.Timestamp, 
                Consumption = readingDto.MeasurementValue,
                UserDeviceId = readingDto.DeviceId };

        }
    }
}
