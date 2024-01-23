using Catalog.Api.Data;
using Catalog.Api.Repositories;
using Catalog.Api.Repositories.Interfaces;
using Catalog.Api.Settings;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
    });
});
builder.Services.Configure<CatalogDatabaseSettings>(
    Configuration.GetSection(nameof(CatalogDatabaseSettings))
    );

builder.Services.AddSingleton<ICatalogDatabaseSettings>(sp =>
   sp.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICatalogContext, CatalogContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
