namespace JwtAuth.Core.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int ProfileId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Profile Profile { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = [];
}
