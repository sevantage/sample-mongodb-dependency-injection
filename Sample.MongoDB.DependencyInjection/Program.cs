using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.MongoDB.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#pragma warning disable CS0618 // Type or member is obsolete
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618 // Type or member is obsolete
builder.Services.Configure<MongoConfiguration>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<IMongoClient>(prov =>
{
    var config = prov.GetRequiredService<IOptions<MongoConfiguration>>().Value;
    var settings = MongoClientSettings.FromConnectionString(config.ConnectionString);
    settings.LinqProvider = MongoDB.Driver.Linq.LinqProvider.V3;
    return new MongoClient(settings);
});
builder.Services.AddSingleton(prov =>
{
    var client = prov.GetRequiredService<IMongoClient>();
    var config = prov.GetRequiredService<IOptions<MongoConfiguration>>().Value;
    return client.GetDatabase(config.DatabaseName);
});
builder.Services.AddSingleton(prov =>
{
    var db = prov.GetRequiredService<IMongoDatabase>();
    return db.GetCollection<MyDocument>("my_documents");
});

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
