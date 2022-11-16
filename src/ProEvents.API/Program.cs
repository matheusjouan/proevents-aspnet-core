using ProEvents.API.Extensions;
using ProEvents.Application.Extensions;
using ProEvents.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Infra
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddRepositories()
    .AddIdentity()
    .AddJwtConfiguration(builder.Configuration);
#endregion

#region Application
builder.Services
    .AddApplicationServices();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFilesConfiguration();

app.UseErrorHandler();

app.Run();
