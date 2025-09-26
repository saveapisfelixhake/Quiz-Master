namespace Backend.Domains.Player.Models.Player;

public class UpdatePlayerDto(string Name , string groupName)
{
    public string Name { get; set; } = Name;
    public string GroupName { get; set; } =  groupName;
}