using Domain.Entities.Abstract;
using Domain.Enums;

namespace Domain.Entities
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
        public ICollection<User>? Users { get; set; }
    }
}
