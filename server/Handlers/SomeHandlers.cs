using core;
using flexiservice;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using OneOf;
using System.Text.Json;

namespace server.Handlers;

[FlexiHandlerFixture]
public class SomeHandlers
{
    public class ExampleStruct
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [FlexiHandler("handlerJson")]
    public OneOf<object, IMessage> HandlerJson(OneOf<string, Any> request)
    {
        var obj = JsonSerializer.Deserialize<ExampleStruct>(request.AsT0);

        obj.FirstName += " foo";
        obj.LastName += " foo";

        return obj;
    }

    [FlexiHandler("handlerAny")]
    public OneOf<object, IMessage> HandlerAny(OneOf<string, Any> request)
    {
        var b = new ExampleMessage();
        var obj = request.AsT1.Unpack<ExampleMessage>();
        obj.FirstName += " foo";
        obj.LastName +=" foo";

        return obj;
    }



}
