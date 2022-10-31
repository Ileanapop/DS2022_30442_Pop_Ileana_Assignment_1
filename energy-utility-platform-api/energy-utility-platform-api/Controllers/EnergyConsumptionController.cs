using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly IEnergyConsumptionService _energyConsumptionService;

        private readonly IMapper _mapper;

        public EnergyConsumptionController(IEnergyConsumptionService energyConsumptionService, IMapper mapper)
        {
            this._energyConsumptionService = energyConsumptionService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] EnergyConsumptionDtoForCreate energyConsumptionDtoForCreate)
        {

            try
            {
                var result = await _energyConsumptionService.Add(energyConsumptionDtoForCreate);

                return Ok(_mapper.Map<EnergyConsumptionViewModel>(result));
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
