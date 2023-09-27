using CoreX.Base;
using Presentation.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureAndAddServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Application.DataSeeder.SeedData(app);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
});

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
app.UseAuthorization();
app.MapControllers();

app.Run();
