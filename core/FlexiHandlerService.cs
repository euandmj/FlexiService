using core.Exceptions;
using flexiservice;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace core;

public interface IFlexiHandlerService
{
    FlexiResponse Get(string target, Any request);
    FlexiResponse Get(string target, string request);
}

public class FlexiHandlerService : IFlexiHandlerService
{
    private readonly HandlerSiteCollection _handlers;

    public FlexiHandlerService(
        IEnumerable<Assembly> assemblies,
        IEnumerable<System.Type> types)
    {
        // TODO!
        IFlexiHandlerBuilder builder = new FlexiHandlerBuilder();
        foreach (var asm in assemblies)
        {
            builder.Add(asm);
        }
        foreach (var t in types)
        {
            builder.Add(t);
        }
        _handlers = builder.Build();
    }

    public FlexiResponse Get(string target, string request)
    {
        if (_handlers.TryGetValue(target, out var site))
        {
            var response = site.Delegate(request);

            Debug.Assert(response.Value is not null);

            return new FlexiResponse { JSON = JsonSerializer.Serialize(response.AsT0) };
        }
        throw new NoSuchHandlerException(target);
    }

    public FlexiResponse Get(string target, Any request)
    {
        if (_handlers.TryGetValue(target, out var site))
        {
            var response = site.Delegate(request);

            Debug.Assert(response.Value is IMessage);

            return new FlexiResponse { Any = Any.Pack(response.AsT1) };
        }
        throw new NoSuchHandlerException(target);
    }



}
