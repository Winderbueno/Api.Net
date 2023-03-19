using User.Domain.Entities.Abstract;

namespace User.Domain.Entities
{
    public class Feature : TrackedEntity
    {
        public int FeatureId { get; set; }

        public string? Name { get; set; }

        public bool Beta { get; set; }

        // Nav | Users
        public ICollection<UserK>? Users { get; set; }

        // Nav | Roles
        public ICollection<Role>? Roles { get; set; }

        // Nav | Permissions
        public ICollection<Permission>? Permissions { get; set; }
    }
}