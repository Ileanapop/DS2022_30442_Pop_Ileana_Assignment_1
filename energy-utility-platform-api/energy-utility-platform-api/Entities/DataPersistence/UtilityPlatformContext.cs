using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace energy_utility_platform_api.Entities.DataPersistence
{
    public class UtilityPlatformContext : DbContext
    {
        public DbSet<EnergyDevice> EnergyDevices { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<EnergyConsumption> EnergyConsumptions { get; set; }

        public UtilityPlatformContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnergyDevice>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Description).HasMaxLength(500).HasDefaultValue("Device description");
                    e.Property(x => x.MaxHourlyEnergy).HasDefaultValue(0);
                    e.HasMany(x => x.UserDevices).WithOne(x => x.EnergyDevice);
                }
                );

            modelBuilder.Entity<User>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Name).HasMaxLength(500).IsRequired();
                    e.Property(x => x.Type).HasMaxLength(100)
                                                .HasDefaultValue(UserTypeEnum.client)
                                                .HasConversion<string>();
                    e.Property(x => x.Password).HasMaxLength(100).IsRequired()
                            .HasConversion(
                        p => PasswordHasher.HashPassword(p),
                        p => p
                        );
                    e.HasMany(x => x.UserDevices).WithOne(x => x.User);
                }
                );

            modelBuilder.Entity<UserDevice>(
                e =>
                {
                    e.ToTable("UserDevices");
                    e.HasKey(x => new {x.UserId, x.EnergyDeviceId});
                    e.Property(x => x.Address).HasMaxLength(500);
                    e.HasOne(x => x.User).WithMany(x => x.UserDevices);
                    e.HasOne(x => x.EnergyDevice).WithMany(x => x.UserDevices);
                    e.HasMany(x => x.EnergyConsumptions).WithOne(x => x.UserDevice);
                }
                );

            modelBuilder.Entity<EnergyConsumption>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.DateTime);
                    e.Property(x => x.Consumption);
                    e.HasOne(x => x.UserDevice).WithMany(x => x.EnergyConsumptions);
                }
                );
        }
    }
}
