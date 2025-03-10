namespace Entities.Templates;

public class ChangePasswordDto
{
    public Guid AppId { get; set; }
    public string NewPassword { get; set; } = null!;
}