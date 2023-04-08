namespace User.Infrastructure.Identity.Dtos
{
    public class ResultResponse<T>
    {
        public bool Succeeded { get; set; }

        public T? Result { get; set; }

        public List<string>? Errors { get; set; }
    }
}
