namespace upLiftUnity_API.MongoModels;
using MongoDB.Driver;
using MongoDB;

public class MongoDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase? _database;

    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
       
        var connectionString = _configuration.GetConnectionString("MConnectionString");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    
   
    
    public IMongoDatabase? Database => _database;
}

