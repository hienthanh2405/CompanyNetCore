
namespace CompanyNetCore.Infrastructure
{
    public class ExceptionReponse
    {
        public string Message { get; set; }

        public ExceptionReponse(string message)
        {
            Message = message;
        }
    }
}
