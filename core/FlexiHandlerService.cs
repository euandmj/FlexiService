using core.Exceptions;
using flexiservice;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static flexiservice.FlexiRequest;

namespace core;

public interface IFlexiHandlerService
{
    FlexiResponse Get(string target, Any request);
    FlexiResponse Get(string target, string request);
}

public class FlexiHandlerService
{
    private readonly FlexiHandlerBuilder _builder = new();

    public FlexiHandlerService(
        IEnumerable<Assembly> assemblies,
        IEnumerable<System.Type> types)
    {
        foreach (var asm in assemblies)
        {
            _builder.AddAssembly(asm);
        }
        foreach (var t in types)
        {
            _builder.AddType(t);
        }
    }

    //public FlexiResponse Get(string target, string request)
    //{
    //    if (_builder.DelegateMap.TryGetValue(target, out var site))
    //    {
    //        var response = site.Delegate(request);

    //        Debug.Assert(response.Value is not null);

    //        return new FlexiResponse { JSON = JsonSerializer.Serialize(response.AsT0) };
    //    }
    //    throw new NoSuchHandlerException(target);
    //}

    //public FlexiResponse Get(string target, Any request)
    //{
    //    if (_builder.DelegateMap.TryGetValue(target, out var site))
    //    {
    //        var response = site.Delegate(request);

    //        Debug.Assert(response.Value is IMessage);

    //        return new FlexiResponse { Any = Any.Pack(response.AsT1) };
    //    }
    //    throw new NoSuchHandlerException(target);
    //}



}
