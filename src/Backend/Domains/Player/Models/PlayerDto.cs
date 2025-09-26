namespace Backend.Domains.Player.Models;

public class PlayerDto(string Name ,string BarName , Guid PlayerId)
{
    public string Name { get; set; } = Name;
    public string BarName { get; set; } = BarName;
    public Guid PlayerId { get; set; } =  PlayerId;
}