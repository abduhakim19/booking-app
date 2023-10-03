namespace API.Utilities.Handlers
{
    public class NotFoundHandler : Exception
    {
        public NotFoundHandler(string message) : base(message) { }
    }
}
