using MongoDB.Bson.Serialization.Attributes;

namespace Servicios.API.Libreria.Core.Entities
{
    [BsonCollection("Empleado")]
    public class EmpleadoEntity : MongoDocument
    {
        [BsonElement("nombre")] 
        public string Nombre { get; set; }
    }
}
