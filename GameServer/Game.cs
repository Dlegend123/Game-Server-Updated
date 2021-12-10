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
            int[] highScores = new int[10];

            for (int i = 0; i < 10; i++)
            {
                highScores[i] = _players[i].Score;
            }
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9 - 1; j++)
                {
                    if (highScores[j] > highScores[j + 1])
                    {
                        (highScores[j + 1], highScores[j]) = (highScores[j], highScores[j + 1]);
                    }
                }
            }

            for (int i = 1; i <= _players.Count; i++)
            {
                for (int j = 1; j <= _players.Count - 1 - 1; j++)
                {
                    if (_players[j].Score > _players[j + 1].Score)
                    {
                        (_players[j + 1].Score, _players[j].Score) = (_players[j].Score, _players[j + 1].Score);
                    }
                }
            }
            int r = 0;
            T[] topPlayers = new T[10];
            for (int i = _players.Count - 1; i >= _players.Count - 10; i--)
            {
                topPlayers[r] = _players[i];
                r++;
            }
            // ... write code that returns 10 players with highest scores
            return topPlayers;
        }
        
    }
}
