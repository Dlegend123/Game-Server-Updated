namespace GameServer
{
    public class PlayerForAnotherGame: IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }
        public PlayerForAnotherGame()
        {
            Items = new List<Item>();
        }
    }
}
