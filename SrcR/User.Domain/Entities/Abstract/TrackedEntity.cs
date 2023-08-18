using User.Domain.Entities.Interfaces;

namespace User.Domain.Entities.Abstract;

public class TrackedEntity : ITrackedEntity
{
  public DateTime CreatedAt { get; set; }
  public string CreatedBy { get; set; } = null!;

  public DateTime ModifiedAt { get; set; }
  public string ModifiedBy { get; set; } = null!;
}