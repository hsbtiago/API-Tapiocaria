using API_MongoDB.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using API_MongoDB.Models;

namespace API_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TapiocasController : ControllerBase
    {
        private Contexto db;

        public TapiocasController(Contexto db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            var lista = await db.Tapiocas.Find(new BsonDocument()).ToListAsync();                
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var filter = Builders<Tapioca>.Filter.Eq(p => p.Id, ObjectId.Parse(id));            
            var model = await db.Tapiocas.Find(filter).FirstAsync();
            
            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tapioca model)
        {
            if (ModelState.IsValid)
            {
                await db.Tapiocas.InsertOneAsync(model);
                return Created(nameof(Get), model);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Tapioca model)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Tapioca>.Filter.Eq(p => p.Id, ObjectId.Parse(id));                    
                
                var update = Builders<Tapioca>.Update
                    .Set(p => p.Recheio, model.Recheio)
                    .Set(p => p.Preco, model.Preco);

                await db.Tapiocas.UpdateOneAsync(filter, update);     

                return Accepted(model);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var filter = Builders<Tapioca>.Filter.Eq(p => p.Id, ObjectId.Parse(id));    
            await db.Tapiocas.FindOneAndDeleteAsync(filter);

            return NoContent();
        }
        
        
    }
}