using Microsoft.EntityFrameworkCore;

namespace NewApp;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EMSContext>();
        await dbContext.Database.MigrateAsync();
    }
}
