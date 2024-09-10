using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.Repository;
using TeaTime.Api.Middlewares;
using TeaTime.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TeaTimeContext>(opt => opt.UseInMemoryDatabase("TeaTimeDb"));
builder.Services.AddHttpClient();

builder.Services.AddScoped<IStoresService, StoreService>();
builder.Services.AddScoped<IOrdersService, OrderService>();

builder.Services.AddScoped<IStoresRepo,APIStoreRepo>();
builder.Services.AddScoped<IOrdersRepo, InMemOrderRepo>();

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

//app.UseMiddleware<ApiAuthMiddleware>(); // 當server時打開

app.MapControllers();

app.Run();
