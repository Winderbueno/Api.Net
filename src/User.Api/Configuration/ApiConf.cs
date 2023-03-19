namespace User.Api.Configuration
{
    public class ApiConf
    {
        public const string SectionKey = "Application";

        public string? Code { get; set; }

        public string? Module { get; set; }

        public Env Environment { get; set; }

        public string? AuthorityUrl { get; set; }

        public string? IdentityApiUrl { get; set; }
    }

    public enum Env
    {
        Dev, Int, Acc, Hot, Prd
    }
}
