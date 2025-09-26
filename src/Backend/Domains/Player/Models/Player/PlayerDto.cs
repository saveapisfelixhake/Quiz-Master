namespace Backend.Domains.Player.Models.Player;

public class PlayerDto(string Name ,string groupName , Guid PlayerId)
{
    public string Name { get; set; } = Name;
    public string GroupName { get; set; } = groupName;
    public Guid PlayerId { get; set; } =  PlayerId;
}