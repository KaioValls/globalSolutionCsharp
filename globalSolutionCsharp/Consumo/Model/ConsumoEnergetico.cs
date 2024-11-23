using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace globalSolutionCsharp.Consumo.Model
{
    public class ConsumoEnergetico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public double Quantidade { get; set; }
        public DateTime DataHora { get; set; }
    }
}
