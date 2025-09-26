namespace Backend.Domains.Player.Models.PlayerGroup;

public class UpdatePlayerGroupDto (string groupName, string barName)
{
public string GroupName { get; set; } = groupName; 
public string BarName { get; set; } = barName;
}