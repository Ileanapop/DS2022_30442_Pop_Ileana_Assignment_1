using Microsoft.EntityFrameworkCore;

namespace energy_utility_platform_api.Entities.DataPersistence
{
    public static class DbPreparation
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<UtilityPlatformContext>());

            }
        }

        public static void SeedData(UtilityPlatformContext context)
        {
            context.Database.Migrate();
            if (!context.Users.Any())
            {
                System.Console.WriteLine("Seeding data...");
                context.Users.AddRange(
                    new User() { Name = "ileana", Password = "ileana", Type = UserTypeEnum.admin },
                    new User() { Name = "mara", Password = "mara", Type = UserTypeEnum.client }
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have data");
            }
        }
    }
}
