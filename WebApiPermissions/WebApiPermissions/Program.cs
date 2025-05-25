using Application.Mapping;
using Application.Permisos.Commands.CreatePermisos;
using Infrastructure;
using Infrastructure.Messaging;

Console.WriteLine("⏳ Delaying startup to wait for Kafka...");
await Task.Delay(TimeSpan.FromSeconds(2)); 

var builder = WebApplication.CreateBuilder(args);

// Kafka Services
//builder.Services.AddSingleton<KafkaProducer>();
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

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
