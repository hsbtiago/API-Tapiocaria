using MongoDB.Bson;

namespace API_MongoDB.Models
{
    public class Tapioca
    {
        public ObjectId Id { get; set; }
        public string Recheio { get; set; }
        public decimal Preco { get; set; }
    }    
}