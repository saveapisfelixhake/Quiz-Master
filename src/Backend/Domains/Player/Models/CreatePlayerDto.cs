namespace Backend.Domains.Player.Models;

public class CreatePlayerDto(string name , string barName)
{
    public string Name { get; set; } = name;
    public string BarName { get; set; } = barName;
}