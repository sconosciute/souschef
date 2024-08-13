namespace souschef_core.Model;

public record UserRole
{
    public long RoleId { get; set; }
    public long UserId { get; set; }
    public Role Role { get; set; }
    public User User { get; set; }
    
}