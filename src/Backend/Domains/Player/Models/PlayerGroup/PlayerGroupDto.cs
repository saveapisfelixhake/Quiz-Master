namespace Backend.Domains.Player.Models.PlayerGroup;

public class PlayerGroupDto(string groupName, string barName, Guid GroupId)
{
    public string GroupName { get; set; } = groupName; 
    public string BarName { get; set; } = barName;
    public Guid GroupId { get; set; } = GroupId;
}
