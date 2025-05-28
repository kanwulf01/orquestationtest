using Application.Interfaces;
using Application.Mapping;
using Application.Permisos.Commands.CreatePermisos;
using Infrastructure;
using Infrastructure.Messaging;
using Nest;

Console.WriteLine("⏳ Delaying startup to wait for Kafka...");
await Task.Delay(TimeSpan.FromSeconds(60)); 
AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.DisableEncryption", true);

var builder = WebApplication.CreateBuilder(args);

// Define una política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPoliticaCors", policy =>
    {
        policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
          //.AllowCredentials();
    });
});


// Kafka Services
builder.Services.AddHostedService<KafkaConsumer>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePermisosCommand).Assembly));

builder.Services.AddAutoMapper(typeof(PermisosProfile).Assembly);

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseCors("MiPoliticaCors");

app.MapControllers();

await app.RunAsync();
