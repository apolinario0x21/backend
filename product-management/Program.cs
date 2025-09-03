using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using ProductStore.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do MongoDB. Pega as informações de appsettings.json ou variáveis de ambiente.
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

// Adiciona uma verificação para garantir que as configurações foram lidas corretamente.
if (mongoDbSettings is null)
{
    throw new InvalidOperationException("MongoDB configuration is missing.");
}

// Injeta o cliente e o banco de dados do MongoDB.
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbSettings.ConnectionString));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbSettings.DatabaseName));

// Adiciona a injeção de dependência dos repositórios, passando o IMongoDatabase para o construtor.
builder.Services.AddSingleton<IProductRepository, ProductRepository>(sp =>
    new ProductRepository(sp.GetRequiredService<IMongoDatabase>()));
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>(sp =>
    new CategoryRepository(sp.GetRequiredService<IMongoDatabase>()));

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
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
}