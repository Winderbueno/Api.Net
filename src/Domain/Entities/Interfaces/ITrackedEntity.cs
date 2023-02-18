namespace Domain.Entities.Interfaces
{
    public interface ITrackedEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }

        DateTime ModifiedAt { get; set; }
        string ModifiedBy { get; set; }
    }
}
