namespace Api.Configuration
{
    public class AppConf
    {
        public const string SectionKey = "Application";

        public string? Code { get; set; }

        public string? Module { get; set; }

        public Env Environment { get; set; }

        public string? AuthorityUrl { get; set; }

        public string? BidApiUrl { get; set; }
    }

    public enum Env
    {
        Dev, Int, Acc, Hot, Prd
    }
}
