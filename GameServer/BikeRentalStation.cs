namespace GameServer
{
    public class BikeRentalStation
    {
        public int id;
        public string name;
        public float x;
        public float y;
        public int bikesAvailable;
        public int spacesAvailable;
        public int capacity;
        public bool allowDropoff;
        public bool allowOverloading;
        public bool isFloatingBike;
        public bool isCarStation;
        public string state;
        public List<string> networks;
        public bool realTimeData;
        public BikeRentalStation()
        {
            name = state = "";
            networks =new List<string>();
        }

        public BikeRentalStation(int id, string name, float x, float y, int bikesAvailable, int spacesAvailable, int capacity, bool allowDropoff, bool allowOverloading, bool isFloatingBike, bool isCarStation, string state, List<string> networks, bool realTimeData)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
            this.bikesAvailable = bikesAvailable;
            this.spacesAvailable = spacesAvailable;
            this.capacity = capacity;
            this.allowDropoff = allowDropoff;
            this.allowOverloading = allowOverloading;
            this.isFloatingBike = isFloatingBike;
            this.isCarStation = isCarStation;
            this.state = state;
            this.networks = networks;
            this.realTimeData = realTimeData;
        }
    }
}
