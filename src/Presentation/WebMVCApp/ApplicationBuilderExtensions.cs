namespace MVCWebApp
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}",
                defaults: new { controller = "Home", action = "Index" });

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
