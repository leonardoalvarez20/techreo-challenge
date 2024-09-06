using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TechreoChallenge.Api.Settings;

namespace TechreoChallenge.Api.Extensions;
public static class MongoDbConfigurationExtension
{
    public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // Read MongoDb settings from appsettings
        var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

        if (mongoDbSettings == null)
            throw new InvalidOperationException("MongoDB settings are not configured.");

        services.AddSingleton(mongoDbSettings);

        //Create service Singleton instance of MongoClient 
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(mongoDbSettings.ConnectionString);
        });

        //Register DB as scoped service
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoDbSettings.DatabaseName);
        });
    }
}
