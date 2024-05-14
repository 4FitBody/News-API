using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using News_Api.Core.Models;
using News_Api.Core.Repositories;
using News_Api.Infrastructure.Repositories;
using News_Api.Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

var blobOptionsSection = builder.Configuration.GetSection("BlobOptions");

var databaseName = builder.Configuration.GetSection("dbName").Get<string>();

var collectionName = builder.Configuration.GetSection("collectionName").Get<string>();

var blobOptions = blobOptionsSection.Get<BlobOptions>() ?? throw new Exception("Couldn't create blob options object");

builder.Services.Configure<BlobOptions>(blobOptionsSection);

var infrastructureAssembly = typeof(NewsMongoRepository).Assembly;

builder.Services.AddMediatR(configurations =>
{
    configurations.RegisterServicesFromAssembly(infrastructureAssembly);
});

var connectionString = builder.Configuration.GetConnectionString("NewsDb");

builder.Services.AddSingleton<INewsRepository>(provider =>
{
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new Exception($"{connectionString} not found");
    }
    return new NewsMongoRepository(connectionString, databaseName, collectionName);
});

builder.Services.AddScoped<INewsRepository, NewsSqlRepository>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "4FitBody (api for working staff)",
        Version = "v1"
    });
});

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasmPolicy", corsBuilder =>
    {
        corsBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope()){
    
     var client = new MongoClient(connectionString);

    var newsDb = client.GetDatabase("NewsDb");

    var newsCollection = newsDb.GetCollection<News>("News");
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("BlazorWasmPolicy");


app.Run();

