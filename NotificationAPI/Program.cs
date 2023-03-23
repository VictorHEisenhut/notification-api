using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NotificationAPI.Controllers.v1;
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationAPI v1", Version = "v1.0" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.CustomSchemaIds(x => x.FullName);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddApiVersioning(options =>
  {
      options.ReportApiVersions = true;
      options.Conventions.Controller<NotificationController>().HasApiVersion(new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0));
  }
);

var connectionString = builder.Configuration.GetConnectionString("NotificationConnection");

builder.Services.AddDbContext<NotificationContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

builder.Services.AddHostedService<RabbitMqSubscriber>();

builder.Services.AddHostedService<RabbitMqDeleteSubscriber>();

builder.Services.AddSingleton<IProcessNotification, ProcessNotification>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1.0");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
