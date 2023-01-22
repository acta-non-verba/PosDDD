using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PublicisPOS.Application.Repositories;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddDbContext<ShoppingCartContext>(options => options.UseInMemoryDatabase("ShoppingCart"));
builder.Services.AddDbContext<InventoryContext>(options => options.UseInMemoryDatabase("Inventory"));
builder.Services.AddDbContext<OrderContext>(options => options.UseInMemoryDatabase("Order"));
builder.Services.AddDbContext<DealContext>(options => options.UseInMemoryDatabase("Deal"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
