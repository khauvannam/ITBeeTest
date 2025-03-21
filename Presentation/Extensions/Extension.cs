using Application.Interfaces.Repositories;
using Application.Mapper;
using Domain.ProjectSettings;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Extensions;

public static class Extension
{
    public static void AddDatabase(this IServiceCollection services)
    {
        // 172.20.0.2
        var conn = ConnString.SqlServer();
        services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(conn));
    }

    public static void AddPersistence(this IServiceCollection services)
    {
        var assembly = typeof(Extension).Assembly;
        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
                    .Json
                    .ReferenceLoopHandling
                    .Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft
                    .Json
                    .NullValueHandling
                    .Ignore;
            });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(typeof(OrderResolver), typeof(OrderDetailResolver));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void UseExternalServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
