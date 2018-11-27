using MongoDB.Bson;

namespace Tapiocaria.Models
{
    public class Tapioca
    {
        public ObjectId Id { get; set; }
        public string Recheio { get; set; }
        public decimal Preco { get; set; }
    }    
}