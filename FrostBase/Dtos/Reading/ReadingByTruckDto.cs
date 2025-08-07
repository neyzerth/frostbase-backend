public class ReadingByTruckDto
{
    public TruckDto Truck { get; set; }
    public ReadingNoTruckDto Reading { get; set; }

    public static ReadingByTruckDto FromModel(TruckReading truckReadings)
    {
        var dto = new ReadingByTruckDto
        {
            Truck = TruckDto.FromModel(truckReadings.Truck),
            Reading = ReadingNoTruckDto.FromModel(truckReadings.Reading)
        };

        return dto;
    }

    public static List<ReadingByTruckDto> FromModel(List<TruckReading> truckReadings)
    {
        var dtos = new List<ReadingByTruckDto>();
        foreach (var truckReading in truckReadings)
        {
            dtos.Add(FromModel(truckReading));
        }
        return dtos;   
    }
}