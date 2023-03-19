using User.Domain.Entities.Abstract;
using User.Domain.Enums;

namespace User.Domain.Entities
{
    public class Role : TrackedEntity
    {
        public int RoleId { get; set; }

        public string? Name { get; set; }

        public UserType UserType { get; set; }

        public Fonction Fonction { get; set; }

        // Nav | Features
        public ICollection<Feature>? Features { get; set; }

        // Nav | Users
        public ICollection<UserK>? Users { get; set; }
    }
}
