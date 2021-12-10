namespace GameServer
{
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        string responseBody;
        HttpClient client;
        public RealTimeCityBikeDataFetcher()
        {
            client = new HttpClient();
            responseBody = client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental").Result;
        }
        public Task<int> GetBikeCountInStation(string stationName)
        {
            List<string> invalidCharacters = new List<string>{ "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            BikeRentalStationList stationList = new BikeRentalStationList();
            stationList= Newtonsoft.Json.JsonConvert.DeserializeObject<BikeRentalStationList>(responseBody);
            if (stationList != null)
            {
                try
                {
                    if (invalidCharacters.Any(x => stationName.Contains(x)))
                    {
                        throw new System.ArgumentException("Invalid character");
                    }

                    int found = 1;
                    for (int i = 0; i < stationList.stations.Count(); i++)
                    {
                        if (stationList.stations[i].name.ToLower().Contains(stationName.ToLower()))
                        {
                            found = 0;
                            return Task.FromResult(stationList.stations[i].bikesAvailable);
                        }
                    }
                    if (found == 1)
                        throw new NotFoundException("Station with that name was not found");
                }
                catch (Exception f)
                {
                    if (f is HttpRequestException || f is ArgumentException)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", f.Message);
                    }
                }
            }
            return Task.FromResult(-1);
        }
    }
}
