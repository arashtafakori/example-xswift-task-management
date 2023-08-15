using Presentation.Configuration;
using MVCWebApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureAndAddServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
Application.DataSeeder.SeedData(app);

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.AddEndpoints();
app.Run();
