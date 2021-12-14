namespace GameServer
{
    internal static class GameHelpers
    {

        public static Action<Player> printItem = (Player player) =>
        {
            Action<Item> printActionDel = MyExtensions.PrintItem;
            MyExtensions.ProcessEachItem(player, printActionDel);
        };
        public static void TryDelegates(List<Player> players)
        {
            Console.WriteLine("Using Delegates");
            Action<Item> printActionDel = MyExtensions.PrintItem;
            foreach (Player user in players)
            {
                MyExtensions.ProcessEachItem(user, printActionDel);
            }
        }
        public static void TryFirstItem(List<Player> players)
        {
            Console.WriteLine("First Item");
            List<Item> temp2 = new List<Item>(players.Count);
            for (int i = 0; i < players.Count; i++)
            {
                temp2.Add( MyExtensions.FirstItem(players[i]));
                Console.WriteLine("Player {2}\n Guid: {0} \n Level: {1}", temp2[i].Id, temp2[i].ItemLevel, i);
            }
        }
        public static void TryFirstItemLinq(List<Player> players)
        {
            Console.WriteLine("First Item Using Linq");
            List<Item> temp2 = new List<Item>(players.Count);
            for (int i = 0; i < players.Count; i++)
            {
                temp2.Add( MyExtensions.FirstItemWithLinq(players[i]));
                Console.WriteLine("Player {2}\n Guid: {0} \n Level: {1}", temp2[i].Id, temp2[i].ItemLevel, i);
            }
        }
        public static void TryGetItems(List<Player> players)
        {
            Console.WriteLine("Get Items");
            List<Item> tempItems;
            for (int i = 0; i < players.Count; i++)
            {
                tempItems = MyExtensions.GetItems(players[i]);
                foreach (Item invent in tempItems)
                {
                    Console.WriteLine("Guid: {0} \n Level: {1}", invent.Id, invent.ItemLevel);
                }
            }
        }
        public static void TryGetItemsLinq(List<Player> players)
        {
            Console.WriteLine("Get Items Using Linq");
            List<Item> tempItems;
            for (int i = 0; i < players.Count; i++)
            {
                tempItems = MyExtensions.GetItemsWithLinq(players[i]);
                foreach (Item invent in tempItems)
                {
                    Console.WriteLine("Guid: {0} \nLevel: {1}", invent.Id, invent.ItemLevel);
                }
            }
        }
        public static Player SetScores(int i, List<Player> players)
        {
            List<int> temp = new List<int>();
            var rand = new Random();
            players.ForEach(x => temp.Add(x.Score));

            while (!temp.Distinct().Contains(players[i].Score))
            {
                players[i].Score = rand.Next();
                temp = new List<int>();
                players.ForEach(x => temp.Add(x.Score));
            }

            return players[i];
        }
        public static List<Player> SetPlayerData(List<Player> players)
        {
            var rand = new Random();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Items = MyExtensions.CreatePlayer();
                players[i].Score = rand.Next();
            }
            for (int i = 0; i < players.Count; i++)
                players[i] = SetScores(i, players);

            return players;
        }
        public static void TryGetItemHigh(List<Player> players)
        {
            List<Item> temp = new(100);
            for (int i = 0; i < players.Count; i++)
            {
                temp.Add(MyExtensions.GetHighestLevelItem(players[i].Items));
                Console.WriteLine("Highest Level Item for Player {2}\n Guid: {0} \n Level: {1}", temp[i].Id, temp[i].ItemLevel, i);
            }
        }
        public static List<Player> InstPlayers()
        {
            List<Player> players = new(100);
            List<Guid> temp = new(100);
            for (int i = 0; i < players.Capacity; i++)
            {
                players.Add(new Player());
                temp.Add(new Guid());
                players[i].Id = temp[i] = Guid.NewGuid();
            }
            int v = 0;
            while (temp.Distinct().Count()!=players.Count)
            {
                while (!temp.Distinct().Contains(players[v].Id))
                {
                    players[v].Id = Guid.NewGuid();
                    temp = new List<Guid>();
                    players.ForEach(x => temp.Add(x.Id));
                }
                v++;
            }
            return players;
        }

        public static void InstGame(List<Player> players)
        {
            PlayerForAnotherGame playersForAnotherGame = new();

            List<Player> players1 = new(players);
            Player temp = new()
            {
                Id = playersForAnotherGame.Id,
                Items = playersForAnotherGame.Items,
                Score = playersForAnotherGame.Score
            };
            players1.Add(temp);
            Game<Player> game = new(players1);

            List<Player> players2 = game.GetTop10Players().ToList();
            Console.WriteLine("Top 10 players: ");
            for (int i = 0; i < players2.Count; i++)
            {
                Console.WriteLine("\nPlayer {0} \nHighscore: {1}", players[2].Id, players2[i].Score);
            }
        }
    }
}