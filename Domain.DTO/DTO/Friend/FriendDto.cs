namespace Domain.DTO.DTO.Friend;

public class FriendDto
{
    public Guid AppId { get; set; }
    public string FriendUsername { get; set; } = null!;
    public string FriendName { get; set; } = null!;
    public DateOnly DateBirth { get; set; }

}