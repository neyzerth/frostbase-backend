public class ParameterDto
{
    public decimal MaxTemperature{ get; set; }
    
    public int MaxHumidity{ get; set; }
    
    public decimal MinTemperature{ get; set; }
    
    public int MinHumidity{ get; set; }
    
    //parsing
    public static ParameterDto FromModel(Parameter p)
    {
        return new ParameterDto
        {
            MaxTemperature = p.MaxTemperature,
            MinTemperature = p.MinTemperature,
            MaxHumidity = p.MaxHumidity,
            MinHumidity = p.MinHumidity,
           
        };
    }
    
}