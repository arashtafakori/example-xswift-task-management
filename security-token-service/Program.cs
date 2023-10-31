using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServerHost.Data;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

//IdentityServerHost.DataSeeder.Seed(connectionString!);

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlServer(connectionString!,
        b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(connectionString!, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(connectionString!, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapDefaultControllerRoute();
});

app.Run();
