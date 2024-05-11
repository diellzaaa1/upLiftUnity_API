using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace upLiftUnity_API.MongoModels
{
    public class Notes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid NoteId { get; set; }
       
        [BsonRepresentation(BsonType.String)]
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

