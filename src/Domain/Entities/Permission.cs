using Domain.Entities.Abstract;

namespace Domain.Entities
{
    public class Permission : TrackedEntity
    {
        public int PermissionId { get; set; }

        public string Name { get; set; }

        // Nav | Features
        public ICollection<Feature> Features { get; set; }
    }
}