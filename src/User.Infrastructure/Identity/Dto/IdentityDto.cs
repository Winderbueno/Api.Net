namespace User.Infrastructure.Identity.Dto
{
    public class IdentityDto
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Mail { get; set; }

        public string? Phone { get; set; }

        public string? Language { get; set; }
    }
}
