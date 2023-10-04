using System.Net;

namespace API.Utilities.Handlers
{
    public class ResponseValidatorHandler
    {   // Properti respinse validator
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        // Contructor untuk inisialisi isi;
        public ResponseValidatorHandler(object error)
        {
            Code = StatusCodes.Status400BadRequest;
            Status = HttpStatusCode.BadRequest.ToString();
            Message = "Validation Error";
            Error = error;
        }
    }
}
