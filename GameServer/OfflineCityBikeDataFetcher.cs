namespace GameServer
{
    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public Task<int> GetBikeCountInStation(string stationName)
        {
            using (StreamReader streamReader = new StreamReader(@"bikedata.txt"))
            {
                int bikeCount = 0, found = 1;

                try
                {
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        if (line.ToLower().Contains(stationName.ToLower()))
                        {
                            found = 0;
                            bikeCount = Int32.Parse(line.Split(":")[1]);
                            return Task.FromResult(bikeCount);
                        }
                    }
                    if (found == 1)
                        throw new NotFoundException("Station with that name was not found");
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                return Task.FromResult(-1);
            }
        }
    }
}
