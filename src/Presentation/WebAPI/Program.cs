using CoreX.Base;
using Doit.AccountModule.Presentation.Configuration;
using Doit.AccountModule.Presentation.WebAPI;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAndAddServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSingleton<IObjectModelValidator, SupressValidationObjectModelValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(typeof(ValidateModelStateAttribute));
//});

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});


var app = builder.Build();
Doit.AccountModule.Application.DataSeeder.SeedData(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
    });
}
app.UseHttpsRedirection();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var devError = ErrorHelper.GetDevError(context.Features.
                Get<IExceptionHandlerPathFeature>()!.Error);

    if (app.Environment.IsProduction())
        await context.Response.WriteAsJsonAsync((Error)devError);
    await context.Response.WriteAsJsonAsync(devError);
}));
app.UseAuthorization();
app.MapControllers();

app.Run();
