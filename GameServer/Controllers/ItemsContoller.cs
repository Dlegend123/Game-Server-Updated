using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameServer.Controllers
{
    [ApiExplorerSettings(GroupName = "Item Options")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ItemsContoller : ControllerBase
    {
      
        private readonly MongoDbRepository repository = new();
        [HttpGet]
        [Route("{id}")]
        public Task<Item> GetItem(Guid PlayerId,Guid ItemId)
        {
            return repository.GetItem(PlayerId, ItemId);
        }
        [HttpGet]
        [Route(".../players/{playerId}/items")]
        public Task<Item[]> GetAll(Guid PlayerId)
        {
            return repository.GetAllItems(PlayerId);
        }
        [HttpPost]
        [Route(".../players/{playerId}/items")]
        public Task<Item> Create(Guid PlayerId,Item item)
        {
            return repository.CreateItem(PlayerId, item);
        }
        [HttpPatch]
        [Route(".../players/{playerId}/items")]
        public Task<Item> Modify(Guid PlayerId, Item item)
        {
            return repository.UpdateItem(PlayerId, item);
        }

        [HttpDelete]
        [Route(".../players/{playerId}/items/{ItemId}")]
        public Task<Item> Delete(Guid PlayerId, Item item)
        {
            return repository.DeleteItem(PlayerId, item);
        }

        // GET api/<ItemsContoller>/5
        /*
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ItemsContoller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ItemsContoller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemsContoller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
