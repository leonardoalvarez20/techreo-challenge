using MongoDB.Bson;
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

    public static void CreateUniqueIndexes(IMongoDatabase database)
    {
        var collection = database.GetCollection<BsonDocument>("Customers");

        var emailIndexKeys = Builders<BsonDocument>.IndexKeys.Ascending("Email");
        var emailIndexOptions = new CreateIndexOptions { Unique = true };
        var emailIndexModel = new CreateIndexModel<BsonDocument>(emailIndexKeys, emailIndexOptions);

        var phoneNumberIndexKeys = Builders<BsonDocument>.IndexKeys.Ascending("PhoneNumber");
        var phoneNumberIndexOptions = new CreateIndexOptions { Unique = true };
        var phoneNumberIndexModel = new CreateIndexModel<BsonDocument>(phoneNumberIndexKeys, phoneNumberIndexOptions);

        var rfcIndexKeys = Builders<BsonDocument>.IndexKeys.Ascending("RFC");
        var rfcIndexOptions = new CreateIndexOptions { Unique = true };
        var rfcIndexModel = new CreateIndexModel<BsonDocument>(rfcIndexKeys, rfcIndexOptions);

        try
        {
            var existingIndexes = collection.Indexes.List().ToList();

            var emailIndexExists = existingIndexes.Any(index => index["name"] == "Email_1");
            var phoneNumberIndexExists = existingIndexes.Any(index => index["name"] == "PhoneNumber_1");
            var rfcIndexExists = existingIndexes.Any(index => index["name"] == "RFC_1");

            if (!emailIndexExists)
            {
                collection.Indexes.CreateOne(emailIndexModel);
                Console.WriteLine("Unique index created on 'Email' field.");
            }

            if (!phoneNumberIndexExists)
            {
                collection.Indexes.CreateOne(phoneNumberIndexModel);
                Console.WriteLine("Unique index created on 'PhoneNumber' field.");
            }

            if (!rfcIndexExists)
            {
                collection.Indexes.CreateOne(rfcIndexModel);
                Console.WriteLine("Unique index created on 'RFC' field.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the indexes: {ex.Message}");
        }
    }
}
