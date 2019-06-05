using System;
using MongoDB.Bson;

namespace Tapiocaria.Models
{
    public class Tapioca
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Recheio { get; set; }
        public decimal Preco { get; set; }
    }    
}