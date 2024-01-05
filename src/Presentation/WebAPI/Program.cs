using XSwift.Base;
using Module.Presentation.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using Module.Presentation.WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureLanguage(builder.Configuration);
builder.Services.AddAuthService(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerService(builder.Configuration);

builder.Logging.AddFilter("Module", LogLevel.Information);

var app = builder.Build();

app.UseSwaggerService();

app.UseHttpsRedirection();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var devError = ErrorHelper.GetDevError(context.Features.
            Get<IExceptionHandlerPathFeature>()!.Error);
 
    devError.RequestId = Activity.Current?.Id ?? context.TraceIdentifier;

    if (app.Environment.IsProduction())
        await context.Response.WriteAsJsonAsync((Error)devError);
    await context.Response.WriteAsJsonAsync(devError);
}));
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
