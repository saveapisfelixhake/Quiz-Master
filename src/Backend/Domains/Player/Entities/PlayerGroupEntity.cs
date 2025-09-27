namespace Backend.Domains.Player.Entities;

public class PlayerGroupEntity(string groupName , string barName, Guid groupId)
{
    public string GroupName { get; set; } = groupName;
    public string BarName { get; set; } = barName;
    public Guid GroupId { get; set; } = groupId;
    
}