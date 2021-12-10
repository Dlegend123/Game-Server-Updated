using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameServer.Controllers
{
    [ApiExplorerSettings(GroupName = "Player Options")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class PlayersController : ControllerBase
    {
        private readonly MongoDbRepository repository = new();
        [HttpGet]
        [Route("{id}")]
        public Task<Player> Get(Guid id)
        {
            return repository.GetPlayer(id);
        }
        [HttpGet]
        [Route("")]
        public Task<Player[]> GetAll()
        {
            return repository.GetAllPlayers();
        }
        [HttpPost]
        [Route("")]
        public Task<Player> Create(Player player)
        {
            return repository.CreatePlayer(player);
        }
        [HttpPatch]
        [Route("")]
        public Task<Player> Modify(Player player)
        {
            return repository.UpdatePlayer(player);
        }
        /*public Task<Player> Modify(Player player)
        {
            ModifiedPlayer modifiedPlayer = new ModifiedPlayer
            {
                Score = player.Score
            };
            return repository.UpdatePlayer(player.Id, modifiedPlayer);
        }*/
        [HttpDelete]
        [Route("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return repository.DeletePlayer(id);
        }
        /*
        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PlayersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
