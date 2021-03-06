using Newtonsoft.Json;

namespace GameServer
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Player: IPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> Items { get; set; }
    }
}
