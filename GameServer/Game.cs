namespace GameServer
{
    public class Game<T> where T : IPlayer
    {
        private readonly List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        public T[] GetTop10Players()
        {
            // ... write code that returns 10 players with highest scores
            return _players.OrderByDescending(x=>x.Score).Take(10).ToArray();
        }
        
    }
}
