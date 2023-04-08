namespace User.Api.Configuration;

public class ConnectionStrings
{
  public const string SectionKey = "ConnectionStrings";

  public string? DefaultConnection { get; set; }
}