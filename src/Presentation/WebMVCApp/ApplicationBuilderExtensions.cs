using Module.Presentation.WebMVCApp.Controllers;

namespace Module.Presentation.WebMVCApp
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: $"{nameof(Home)}/{nameof(Home.Index)}",
                    defaults: new { controller = nameof(Home), action = nameof(Home.Index) });

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
