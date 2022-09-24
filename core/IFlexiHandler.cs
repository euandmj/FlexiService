using Google.Protobuf.WellKnownTypes;
using OneOf;
using Google.Protobuf;

namespace core;

public interface IFlexiHandler
{
    public string HandlerName {get;}
    public Task<OneOf<string, IMessage>> Handle(
        OneOf<string, Any> request,
        CancellationToken token
    );
    // public Task<TResponse> Handle(
    //     TRequest request,
    //     CancellationToken token
    // );
}

// public interface IFlexiHandlerJson<TResponse> : IFlexiHandler<string, TResponse> {}
// public interface IFlexiHandlerAny : IFlexiHandler<Any, Any> {}