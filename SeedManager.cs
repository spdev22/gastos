
public static class WebApplicationExtensions
{
    public static WebApplication SeedData(this WebApplication app)
    {
        IServiceScopeFactory scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
            seeder.SeedDbAsync().Wait();
        }
        return app;
    }
}