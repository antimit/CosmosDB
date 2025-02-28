using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestApp2._0.Data;
using TestApp2._0.Models;
using TestApp2._0.Services;
using AutoMapper;
using Microsoft.Azure.Cosmos;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 🔹 Replace SQL Server with CosmosDB
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseCosmos(
                builder.Configuration["CosmosDb:Endpoint"],     // CosmosDB URL
                builder.Configuration["CosmosDb:Key"],          // CosmosDB Primary Key
                builder.Configuration["CosmosDb:DatabaseName"]  // Database Name
            ));
        
        builder.Services.AddSingleton<CosmosClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var endpoint = configuration["CosmosDb:Endpoint"];
            var key = configuration["CosmosDb:Key"];

            return new CosmosClient(endpoint, key);
        });


        // 🔹 Register Services
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<VehicleService>();
        builder.Services.AddScoped<DriverService>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<DeliveryItemService>();
        builder.Services.AddScoped<DeliveryService>();
        builder.Services.AddScoped<AddressService>();
        builder.Services.AddScoped<StopService>();
        builder.Services.AddScoped<TransportationService>();

        

        // 🔹 AutoMapper Configuration
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // 🔹 Ensure CosmosDB is Ready (No Migration Needed)
        await EnsureCosmosDbCreatedAsync(app);

        // app.MigrateDbAsync();

        app.Run();
    }

    // /// 🔹 Ensure CosmosDB Database is Created (CosmosDB Doesn't Have Migrations)
    private static async Task EnsureCosmosDbCreatedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
}
