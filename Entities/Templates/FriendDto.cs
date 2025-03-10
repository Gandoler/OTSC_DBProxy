namespace Entities.Templates;

public class FriendDto
{
    public Guid AppId { get; set; }
    public string FriendUsername { get; set; } = null!;
    public string FriendName { get; set; } = null!;
    public DateTime DateBirth { get; set; }
    public string? Interest { get; set; }
    public string? Pozhelanie { get; set; }

}