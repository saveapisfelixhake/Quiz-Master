namespace Backend.Domains.Player.Entities;

public class PlayerEntity(string name, string groupName, Guid playerId)
{
    public string Name { get; set; } = name;
    public string GroupName { get; set; } = groupName;
    public Guid PlayerId { get; set; } = playerId;
    
}