using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Tapiocaria.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Tapiocaria.Data
{
    public class Contexto
    {
        public IMongoCollection<Tapioca> Tapiocas {get; private set;}

        public Contexto([FromServices]IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("mongo"));
            
            var db = client.GetDatabase("Tapiocaria");

            Tapiocas = db.GetCollection<Tapioca>("Tapiocas");
        }
    }

    public static class IMongoCollectionExtension
    {
        public static async Task ExcluirAsync<T>(this IMongoCollection<T> collection, string id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("Id", id);    
            await collection.DeleteOneAsync(filter);
        }

        public static async Task<T> BuscarAsync<T>(this IMongoCollection<T> collection, string id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("Id", id);    
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public static async Task<IList<T>> BuscarTodosAsync<T>(this IMongoCollection<T> collection) where T : class
        {            
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}