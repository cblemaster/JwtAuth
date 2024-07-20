namespace JwtAuth.Core.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string Rolename { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = [];
}
