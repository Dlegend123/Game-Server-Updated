namespace GameServer
{
    public class FileRepository: IRepository
    {
        public Task<Player> GetPlayer(Guid id)
        {
            string texts = File.ReadAllText(@"game-dev.txt");
            Player player1 = new Player();
            List<Player> players = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Player>>(texts);
            try
            {
                foreach (Player player in players)
                {
                    if (player.Id == id)
                        return Task.FromResult(player);
                }
                throw new NotFoundException("The player was not found");
            }
            catch (NotFoundException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Task.FromResult(player1);
        }

        public Task<Player[]> GetAllPlayers()
        {
            var lines = File.ReadAllText(@"game-dev.txt");

            return Task.FromResult(Newtonsoft.Json.JsonConvert.DeserializeObject<Player[]>(lines));
        }

        public Task<Player> CreatePlayer(Player player)
        {
            if (!File.Exists(@"game-dev.txt"))
                File.WriteAllText(@"game-dev.txt", Newtonsoft.Json.JsonConvert.SerializeObject(player));
            else
                File.AppendAllText(@"game-dev.txt", Newtonsoft.Json.JsonConvert.SerializeObject(player));
            return Task.FromResult(player);
        }

        public Task<Player> UpdatePlayer(Guid id, ModifiedPlayer player)
        {
            string texts = File.ReadAllText(@"game-dev.txt");
            List<Player> players = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Player>>(texts);
            Player playerTemp = players.Where(x => x.Id == id).First();
            int index = 0;
            if (playerTemp != null)
            {
                players[players.IndexOf(playerTemp)].Score = player.Score;
                File.WriteAllText(@"game-dev.txt", Newtonsoft.Json.JsonConvert.SerializeObject(players));
            }
            return Task.FromResult(players[index]);
        }

        public Task<Player> DeletePlayer(Guid id)
        {
            string texts = File.ReadAllText(@"game-dev.txt");
            List<Player> players = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Player>>(texts);
            var PlayerTemp = players.Where(x => x.Id == id).First();
            players.Remove(PlayerTemp);
            File.WriteAllText(@"game-dev.txt", Newtonsoft.Json.JsonConvert.SerializeObject(players));
            return Task.FromResult(PlayerTemp);
        }

        Task<Player> IRepository.UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        Task<Item> IRepository.CreateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        Task<Item> IRepository.GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        Task<Item[]> IRepository.GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        Task<Item> IRepository.UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        Task<Item> IRepository.DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
