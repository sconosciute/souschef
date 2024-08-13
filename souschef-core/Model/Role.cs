namespace souschef_core.Model;

public record Role : IDbModel
{
    public long RoleId { get; set; }
    
    public string RoleName { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
}