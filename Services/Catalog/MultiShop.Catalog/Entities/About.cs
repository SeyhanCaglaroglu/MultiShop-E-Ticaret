using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
    public class About
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AboutId { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
