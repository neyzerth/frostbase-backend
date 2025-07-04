using FrostBase.Models.Alert;

namespace FrostBase.Dtos.Alert;

public class CreateAlert
{
    public int Id { get; set; }
    public string State { get; set; }
    public DateTime Date { get; set; }
    public decimal DetectedValue { get; set; }
    public AlertType AlertType { get; set; }
    public Truck Truck { get; set; }
}