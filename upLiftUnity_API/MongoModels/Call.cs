using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace upLiftUnity_API.MongoModels
{
    public class Call
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Use ObjectId representation for ID
        public ObjectId Id { get; set; } 

        [BsonElement("CallerNickname"), BsonRepresentation(BsonType.String)]
        public string CallerNickname { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonRepresentation(BsonType.String)]
        public int RiskLevel { get; set; }
            //shto daten e thirrjes veq ne back by default dita e sotit
            //provo vet id me bo autoincrement veq me ni variabel ose me marr thirrjen e fundit ...
        
    }
}
