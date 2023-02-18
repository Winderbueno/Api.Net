namespace Api.Configuration
{
    public class AppConfiguration
    {
        public const string SectionKey = "Application";

        public string Code { get; set; } = null!;

        public string Module { get; set; } = null!;

        public Env Environment { get; set; }

        public string AuthorityUrl { get; set; }

        public string BidApiUrl { get; set; }
    }

    public enum Env
    {
        Dev, Int, Acc, Hot, Prd
    }
}
