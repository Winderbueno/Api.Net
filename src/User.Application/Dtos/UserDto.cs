namespace User.Application.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Mail { get; set; }

        public string? Phone { get; set; }

        public int RoleId { get; set; }

        public IEnumerable<string>? Permissions { get; set; }
    }
}
