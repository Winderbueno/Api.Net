namespace User.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string entity, string data, string value)
            : base($"'{entity}' with {data} : {value}, not found in db") { }
    }
}
