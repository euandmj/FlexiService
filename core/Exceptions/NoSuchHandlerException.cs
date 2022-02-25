namespace core.Exceptions;

internal class NoSuchHandlerException : Exception
{
    public string HandlerName { get; }

    public NoSuchHandlerException(string handlerName)
        : base("No such flexi handler was found.")
    {
        HandlerName = handlerName;
    }
}
