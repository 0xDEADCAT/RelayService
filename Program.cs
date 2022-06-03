using System.Reflection;
using RelayService.Hubs;
using Microsoft.AspNetCore.HttpOverrides;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(prefix: "HPDS_COMMON_");

// Add services to the container.
builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = null;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["HPDS_COMMON_RABBITMQ_HOST"], builder.Configuration["HPDS_COMMON_RABBITMQ_VHOST"], h => {
            h.Username(builder.Configuration["HPDS_COMMON_RABBITMQ_USERNAME"]);
            h.Password(builder.Configuration["HPDS_COMMON_RABBITMQ_PASSWORD"]);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

// UseCors must be called before MapHub.
app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run();