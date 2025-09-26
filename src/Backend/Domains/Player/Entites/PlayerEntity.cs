namespace Backend.Domains.Player.Entites;

public class PlayerEntity(string name, string barName, Guid playerId)
{
    public string Name { get; set; } = name;
    public string BarName { get; set; } = barName;
    public Guid PlayerId { get; set; } = playerId;
    
}