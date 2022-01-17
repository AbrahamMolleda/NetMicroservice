using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Servicios.API.Libreria.Core.Entities
{
    [BsonCollection("Libro")]
    public class LibroEntity : MongoDocument
    {
        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("precio")]
        public int Precio { get; set; }

        [BsonElement("fechaPublicacion")]
        public DateTime FechaPublicacion { get; set; }

        [BsonElement("autor")]
        public AutorEntity Autor { get; set; }
    }
}
