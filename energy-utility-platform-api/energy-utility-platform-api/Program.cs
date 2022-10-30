using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.Repositories;
using energy_utility_platform_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UtilityPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEnergyDeviceService, EnergyDeviceService>();
builder.Services.AddTransient<IEnergyDeviceRepository, EnergyDeviceRepository>();
builder.Services.AddTransient<IUserDeviceService, UserDeviceService>();
builder.Services.AddTransient<IUserDeviceRepository, UserDeviceRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:44384",
            ValidAudience = "https://localhost:44384",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5bd5c3d0-21db-4ddd-bda1-602e3dfd96d2"))
        };
    }
    );

builder.Services.AddAuthorization(config =>
    {
        config.AddPolicy(Policies.Client, policy =>
            policy.Requirements.Add(new PrivilegeRequirement(Policies.Client))
        );
        config.AddPolicy(Policies.Admin, policy =>
           policy.Requirements.Add(new PrivilegeRequirement(Policies.Admin))
       );
    }
);

builder.Services.AddSwaggerGen(c =>

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Plaease insert JWT with Bearer into field",
        Name = "Bearer",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
    })
    ); ;

builder.Services.AddSwaggerGen(c =>
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    }
    ));


var app = builder.Build();

//app.UseSession();
app.UseMiddleware<Authentication>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
