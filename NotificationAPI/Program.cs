using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotificationAPI.Data;
using NotificationAPI.EventProcessor;
using NotificationAPI.RabbitMqClient.Client;
using NotificationAPI.RabbitMqClient.Consumers;
using NotificationAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("NotificationConnection");

builder.Services.AddDbContext<NotificationContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

builder.Services.AddHostedService<RabbitMqSubscriber>();

builder.Services.AddSingleton<IProcessNotification, ProcessNotification>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
