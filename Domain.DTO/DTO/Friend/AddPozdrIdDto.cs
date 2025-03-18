namespace Domain.DTO.DTO.Friend;

public class AddPozdrIdDto
{
   

    public int PozdrikId{ get; set; }
    public string FriendUsername{ get; set; } = String.Empty;
    public Guid AppId{ get; set; }
}