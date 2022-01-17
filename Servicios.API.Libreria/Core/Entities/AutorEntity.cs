using MongoDB.Bson.Serialization.Attributes;

namespace Servicios.API.Libreria.Core.Entities
{
    [BsonCollection("Autor")]
    public class AutorEntity : MongoDocument
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("gradoAcademico")]
        public string GradoAcademico { get; set; }
    }
}
