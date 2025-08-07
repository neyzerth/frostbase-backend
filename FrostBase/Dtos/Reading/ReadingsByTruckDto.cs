public class ReadingsByTruckDto
{
    public TruckDto Truck { get; set; }
    public List<ReadingNoTruckDto> Readings { get; set; }

    public static ReadingsByTruckDto FromModel(TruckReadings truckReadings)
    {
        var dto = new ReadingsByTruckDto
        {
            Truck = TruckDto.FromModel(truckReadings.Truck),
            Readings = ReadingNoTruckDto.FromModel(truckReadings.Readings)
        };

        return dto;
    }

    public static List<ReadingsByTruckDto> FromModel(List<TruckReadings> truckReadings)
    {
        var dtos = new List<ReadingsByTruckDto>();
        foreach (var truckReading in truckReadings)
        {
            dtos.Add(FromModel(truckReading));
        }
        return dtos;   
    }
}