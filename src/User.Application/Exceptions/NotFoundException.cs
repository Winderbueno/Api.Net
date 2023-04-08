namespace User.Application.Exceptions;

public class NotFoundException : Exception
{
  public NotFoundException(string message) : base(message) { }

  public NotFoundException(string entity, string data, string value)
      : base($"'{entity}' with {data} : {value}, not found in db") { }
}