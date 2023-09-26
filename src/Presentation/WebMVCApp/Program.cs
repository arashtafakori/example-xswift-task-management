using Presentation.Configuration;
using Presentation.WebMVCApp;
using CoreX.Mvc;
using Presentation.WebMVCApp.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureAndAddServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
Application.DataSeeder.SeedData(app);

app.UseExceptionHandler("/" + nameof(Home) + "/" + nameof(Home.Error));
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.AddEndpoints();
app.Run();
