using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace upLiftUnity_API.MongoModels
{
    public class Call
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Use ObjectId representation for ID
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid CallId { get; set; }

        [BsonElement("CallerNickname"), BsonRepresentation(BsonType.String)]
        public string CallerNickname { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonRepresentation(BsonType.String)]
        public int RiskLevel { get; set; }
         
    }
}
