using User.Domain.Entities.Abstract;

namespace User.Domain.Entities;

public class UserK : TrackedEntity
{
  public int UserKId { get; set; }

  // Id de l'identite dans Identity Services
  public Guid IdentityId { get; set; }

  // Nav | Role
  public int RoleId { get; set; }
  public Role? Role { get; set; }

  // Nav | Features
  public ICollection<Feature>? BetaFeatures { get; set; }
}