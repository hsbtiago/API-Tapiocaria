using MongoDB.Bson;
using MongoDB.Driver;
using Tapiocaria.Data;
using Tapiocaria.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Tapiocaria.Controllers
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
            var lista = await db.Tapiocas.BuscarTodosAsync();                
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {            
            var model = await db.Tapiocas.BuscarAsync(id);
            
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
        public async Task<IActionResult> Put([FromBody] Tapioca model)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Tapioca>.Filter.Eq(p => p.Id, model.Id);                    
                
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
            await db.Tapiocas.ExcluirAsync(id);
            return NoContent();
        }
        
        
    }
}