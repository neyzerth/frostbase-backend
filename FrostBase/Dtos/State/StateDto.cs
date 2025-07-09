public class StateDto
{
    public string Id { get; set; }
    public string Description { get; set; }

    public StateDto FromModel(State s)
    {
        return new StateDto();
    }
}