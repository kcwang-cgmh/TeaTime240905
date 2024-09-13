using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.Repositories;
using TeaTime.Api.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<IStoresService, StoresService>();
        builder.Services.AddScoped<IOrdersService, OrdersService>();

        builder.Services.AddScoped<IStoresRepository, ApiStoresRepository>();
        builder.Services.AddScoped<IOrdersRepository, ApiOrdersRepository>();





        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}