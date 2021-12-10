namespace GameServer
{
    public class BikeRentalStationList
    {
        public BikeRentalStation[] stations;
        public BikeRentalStationList()
        {
            stations = new BikeRentalStation[] {};
        }

        public BikeRentalStationList(BikeRentalStation[] stations)
        {
            this.stations = stations;
        }
    }
}
