using MongoDB.Bson;
using MongoDB.Driver;

namespace GameServer
{
    public class MongoDbRepository: IRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<Player> players;
        private IMongoCollection<Item> items;
        private List<string> Tags;
        public MongoDbRepository()
        {
            var _mongoClient = new MongoClient("mongodb+srv://name:password@cluster0.dd3zz.mongodb.net/game?retryWrites=true&w=majority");
            database = _mongoClient.GetDatabase("game");
            players = database.GetCollection<Player>("players");
            items = database.GetCollection<Item>("items");
            Tags = new List<string> { "123", "legend", "heart" };
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await players.InsertOneAsync(player);
            return player;
        }

        public Task<Player> DeletePlayer(Guid playerId)
        {
            Player player = new();
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", playerId);
            if (!players.Find(filter).Any())
                Task.FromException(new NotFoundException("Player not found"));
            players.DeleteOneAsync(filter).Wait();
            return Task.FromResult(player);
        }

        public Task<Player[]> GetAllPlayers()
        {
            if (players.ToBson().Length==0)
                return Task.FromException<Player[]>(new NotFoundException("There are no players"));
            List<Player> list = players.Find(new BsonDocument()).ToList();
            return Task.FromResult(list.ToArray());
        }

        public Task<Player> GetPlayer(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", playerId);

            if (players == null)
                return Task.FromException<Player>(new NotFoundException("There are no players"));
            if (!players.Find(filter).Any())
                return Task.FromException<Player>(new NotFoundException("Player was not found"));
            return Task.FromResult(players.Find(filter).Single());
        }

        public Task<Player> UpdatePlayer(Player player)
        {
            var filter = Builders<Player>.Filter.Eq("Id", player.Id);
            if (!players.Find(filter).Any())
                return Task.FromException<Player>(new NotFoundException("Player was not found"));
            players.ReplaceOneAsync(filter, player).Wait();
            return Task.FromResult(player);
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);

            if (!players.Find(filter).Any())
                return Task.FromException<Item>(new NotFoundException("Player not found"));

            if (players.Find(filter).Single().Level < 3 && item.Type.Equals(ItemType.SWORD))
                return Task.FromException<Item>(new TooLowLevelException("Player level is too low"));
            try
            {
                items.InsertOneAsync(item).Wait();
            }
            catch (MongoWriteConcernException e)
            {
                if (e.Code == 11000)
                    return Task.FromException<Item>(new YourConflictException("Player already has that item"));
            }
            return Task.FromResult(item);
        }
        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);

            if (!players.Find(filter).Any())
                return Task.FromException<Item>(new NotFoundException("Player not found"));
            var player = players.Find(filter).Single();
            List<Item> iList = player.Items;
            iList[iList.IndexOf(iList.Where(x => x.Id == item.Id).First())] = item;

            player.Items = iList;
            players.ReplaceOneAsync(filter, player).Wait();
            var filter2 = Builders<Item>.Filter.Eq("Id", item.Id);
            if (!items.Find(filter2).Any())
                return Task.FromException<Item>(new NotFoundException("Item not found"));
            items.ReplaceOneAsync(filter2, item).Wait();
            return Task.FromResult(item);
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", itemId);
            if (!items.Find(filter).Any())
                return Task.FromException<Item>(new NotFoundException("Item not found"));
            return Task.FromResult(items.Find(filter).Single());
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("OwnerId", playerId);
            if (!items.Find(filter).Any())
                return Task.FromException<Item[]>(new NotFoundException("No items found"));
            return Task.FromResult(items.Find(filter).ToList().ToArray());
        }
        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", item.Id);
            if (!items.Find(filter).Any())
                return Task.FromException<Item>(new NotFoundException("Item not found"));
            items.DeleteOneAsync(filter).Wait();
            return Task.FromResult<Item>(items.Find(filter).Single());
        }
        public Task<Player[]> GetSorted()
        {
            List<Player> pList = players.Find(filter => filter.Score > 5000).SortByDescending(x=>x.Score).ToList();
            return Task.FromResult(pList.Take(10).ToArray());
        }
        public Task<Player[]> GetTagged()
        {
            List<Player> PList = new();

            foreach (Player player in players.AsQueryable())
            {
                if (Tags.Any(player.Name.Contains))
                    PList.Add(player);
            }
            return Task.FromResult(PList.ToArray());
        }
        public Task<Player[]> GetPlayers()
        {
            List<Guid> guids = new();
            List<Item> list = items.Find(filter => filter.Type == ItemType.SHIELD).ToList();

            foreach (Item item in list)
            {
                if (item.Type == ItemType.SHIELD)
                    guids.Add(item.OwnerId);
            }
            return Task.FromResult(players.Find(player => player.Id.CompareTo(guids.Any()) == 0)
                .ToList().ToArray());

        }
    }
}
