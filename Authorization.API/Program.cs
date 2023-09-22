using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using Authorization.Common.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions()
    .AddHttpContextAccessor()
    .AddControllers();

builder.Services.AddCustomDbContext(builder.Configuration)
    .AddCustomAuthentication(builder.Configuration);

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(HandleGlobalExceptions);

app.MapControllers();

app.Run();

void HandleGlobalExceptions(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger(nameof(Program));
            logger?.LogError($"{contextFeature.Error}");

            if (contextFeature.Error.GetType() == typeof(UnauthorizedAccessException))
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                StatusMessage = Convert.ToString(contextFeature.Error.GetType()),
                Error = contextFeature?.Error?.Message,
            }));
        }
    });
}