public class StateDto
{
    public string Id { get; set; }
    public string Description { get; set; }

    public static StateDto FromModel(State s)
    {
        return new StateDto
        {
            Id = s.Id,
            Description = s.Message
        };
    }
}