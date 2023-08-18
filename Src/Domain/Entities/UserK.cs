using Domain.Entities.Abstract;

namespace Domain.Entities;

public class UserK : TrackedEntity
{
  public int UserKId { get; set; }

  // Id de l'identite dans Identity Services
  public Guid IdentityId { get; set; }

  // Navigations
  public int RoleId { get; set; }
  public Role? Role { get; set; }
  public ICollection<Feature>? BetaFeatures { get; set; }
}