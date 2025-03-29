using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WURS.Constants;
using WURS.Extensions;
using WURS.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
configuration.AddEnvironmentVariables();

// CORS
services.AddCorsPolicy(configuration);

//Identity
services.AddIdentity(configuration);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthorization();

services.AddCustomOptions();

var app = builder.Build();

await app.AddContextsAsync();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(ConfigurationSections.CorsSection);

app.UseHttpsRedirection();

app.UseCustomMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.Run();
