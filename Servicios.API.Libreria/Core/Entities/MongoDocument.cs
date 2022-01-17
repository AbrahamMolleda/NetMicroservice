using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Servicios.API.Libreria.Core.Entities
{
    public class MongoDocument : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}
