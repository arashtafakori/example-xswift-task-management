using Module.Presentation.Configuration;
using Module.Presentation.WebMVCApp;
using Module.Presentation.WebMVCApp.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureLanguage(builder.Configuration);
builder.Services.HttpClientService(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler("/" + nameof(Home) + "/" + nameof(Home.Error));
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.AddEndpoints();

app.Run();
