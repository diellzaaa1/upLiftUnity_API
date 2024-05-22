using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace upLiftUnity_API.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid NotificationId { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; } = false;   

        public DateTime CreatedOnUtc { get; set; }
    }
}
