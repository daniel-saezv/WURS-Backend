using Microsoft.AspNetCore.Identity;
using WURS.Business.Configuration.Models;
using WURS.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
configuration.AddEnvironmentVariables();

services.AddCorsPolicy(configuration)
    .AddIdentity(configuration)
    .AddSwaggerGen()
    .AddAuthorization()
    .AddOptionsBinding()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

await app.AddContextsAsync();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(DefaultCorsPolicy.SectionName)
    .UseHttpsRedirection()
    .UseCustomMiddlewares()
    .UseAuthorization();

app.MapControllers();

app.Run();
