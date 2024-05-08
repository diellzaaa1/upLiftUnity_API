using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace upLiftUnity_API.MongoModels
{
    public class Call
    {
       
            [BsonId]
            public int Id { get; set; }

            [BsonElement("CallerNickname"), BsonRepresentation(BsonType.String)]
            public string CallerNickname { get; set; }

            [BsonRepresentation(BsonType.String)]
            public string Description { get; set; }

            [BsonRepresentation(BsonType.String)]
            public int RiskLevel { get; set; }
        
    }
}
