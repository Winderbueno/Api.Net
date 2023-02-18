using Domain.Entities.Abstract;

namespace Domain.Entities
{
    public class User : TrackedEntity
    {
        public int UserId { get; set; }

        // Id de l'identite dans Identity Services
        public Guid IdentityId { get; set; }

        // Nav | Role
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Nav | Features
        public ICollection<Feature> BetaFeatures { get; set; }
    }
}