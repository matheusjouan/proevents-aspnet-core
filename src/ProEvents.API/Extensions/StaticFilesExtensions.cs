using Microsoft.Extensions.FileProviders;

namespace ProEvents.API.Extensions;

public static class StaticFilesExtensions
{
    public static void UseStaticFilesConfiguration(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
            RequestPath = new PathString("/Resources")
        });
    }
}
