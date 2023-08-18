using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Feature : TrackedEntity
{
  public int FeatureId { get; set; }

  public string? Name { get; set; }
  public bool Beta { get; set; }

  // Navigations
  public ICollection<UserK>? Users { get; set; }
  public ICollection<Role>? Roles { get; set; }
  public ICollection<Permission>? Permissions { get; set; }
}