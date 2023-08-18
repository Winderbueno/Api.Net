using Domain.Entities.Abstract;
using Domain.Enums;

namespace Domain.Entities;

public class Role : TrackedEntity
{
  public int RoleId { get; set; }

  public string? Name { get; set; }
  public UserType UserType { get; set; }
  public UserFunction Function { get; set; }

  // Navigations
  public ICollection<Feature>? Features { get; set; }
  public ICollection<UserK>? Users { get; set; }
}