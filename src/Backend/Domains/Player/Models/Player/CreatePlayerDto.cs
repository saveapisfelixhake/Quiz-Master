namespace Backend.Domains.Player.Models.Player;

public class CreatePlayerDto(string name , string groupName)
{
    public string Name { get; set; } = name;
    public string GroupName { get; set; } = groupName;
}