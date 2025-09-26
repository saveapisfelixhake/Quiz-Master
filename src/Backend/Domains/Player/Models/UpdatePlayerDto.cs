namespace Backend.Domains.Player.Models;

public class UpdatePlayerDto(string Name , string BarName)
{
    public string Name { get; set; } = Name;
    public string BarName { get; set; } =  BarName;
}