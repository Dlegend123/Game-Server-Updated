namespace GameServer
{
    public class MyExtensions
    {
        public static Item GetHighestLevelItem(List<Item> Items)
        {
            int max = Items[0].ItemLevel;
            int index = 0;

            for (int i = 1; i < Items.Capacity; i++)
            {
                if (max < Items[i].ItemLevel)
                {
                    max = Items[i].ItemLevel;
                    index = i;
                }

            }
            return Items[index];
        }
        public static List<Item> CreatePlayer()
        {
            var rand = new Random();

            Item sword = new()
            {
                Id = Guid.NewGuid(),
                ItemLevel = rand.Next()
            };
            Item gloves = new()
            {
                Id = Guid.NewGuid(),
                ItemLevel = rand.Next()
            };
            Item helmet = new()
            {
                Id = Guid.NewGuid(),
                ItemLevel = rand.Next()
            };
            Item shoes = new()
            {
                Id = Guid.NewGuid(),
                ItemLevel = rand.Next()
            };
            List<Item> Items = new()
            {
                sword,
                gloves,
                helmet,
                shoes
            };
            return Items;
        }
        public static List<Item> GetItems(Player player)
        {
            List<Item> items = new List<Item>(player.Items.Count);
            for (int i = 0; i < player.Items.Count; i++)
            {
                items.Add( player.Items[i]);
            }

            return items;
        }
        public static List<Item> GetItemsWithLinq(Player player)
        {
            return player.Items;
        }
        public static Item FirstItem(Player player)
        {
            return (!(player.Items.Count == 0)) ? player.Items[0] : null;
        }
        public static Item FirstItemWithLinq(Player player)
        {
            return (!(player.Items.Count() == 0)) ? player.Items.First() : null;
        }
        public static void PrintItem(Item item)
        {
            Console.WriteLine("Guid: {0} \nLevel: {1}", item.Id, item.ItemLevel);
        }
        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item item in player.Items)
            {
                process(item);
            }
        }
    }
}
