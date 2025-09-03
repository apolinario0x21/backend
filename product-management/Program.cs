using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductStore.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do MongoDB. Pega as informações de appsettings.json ou variáveis de ambiente.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

// Adiciona a injeção de dependência dos repositórios
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

// Classe para as configurações do MongoDB
public class MongoDbSettings
{
    [Required]
    public string? ConnectionString { get; set; }
    [Required]
    public string? DatabaseName { get; set; }
}