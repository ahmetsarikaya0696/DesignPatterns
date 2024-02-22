using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace StrategyDesingPattern.Models
{
    public class Product
    {
        [Key, BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [Precision(18, 2), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
    }
}
