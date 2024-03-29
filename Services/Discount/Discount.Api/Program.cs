using Discount.Api.Repositories;
using Discount.Api.Repositories.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {  Title = "Discount.Api", Version = "v1" });
});

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.Api v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
