using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Entities
{
        public abstract class MongoBaseEntity
        {
            public string Id { get; set; }
        }
    
}
