using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Tapiocaria.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration; 

namespace Tapiocaria.Data
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

    public static class IMongoCollectionExtension
    {
        public static async Task ExcluirAsync<T>(this IMongoCollection<T> collection, string id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));    
            await collection.DeleteOneAsync(filter);
        }

        public static async Task<T> BuscarAsync<T>(this IMongoCollection<T> collection, string id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));    
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public static async Task<IList<T>> BuscarTodosAsync<T>(this IMongoCollection<T> collection) where T : class
        {            
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}