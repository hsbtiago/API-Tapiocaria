using System.Linq;
using MongoDB.Driver;
using API_MongoDB.Models;
using Microsoft.Extensions.Configuration;

namespace API_MongoDB.Data
{
    public class Contexto
    {
        private IConfiguration _configuration;        
        public IMongoCollection<Tapioca> Tapiocas {get; private set;}

        public Contexto(IConfiguration config)
        {
            _configuration = config;

            var client = new MongoClient(_configuration.GetConnectionString("localhost"));
            
            var db = client.GetDatabase("lanchonete");

            Tapiocas = db.GetCollection<Tapioca>("tapiocas");
        }
    }
}