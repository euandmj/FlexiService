using core;
using flexiservice;
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

    [FlexiHandler("handlerJson", FlexiHandlerScope.Lifetime)]
    public FlexiResponse HandlerJson(FlexiRequest request)
    {
        var obj = request.PayloadCase switch
        {
            FlexiRequest.PayloadOneofCase.JSON => JsonSerializer.Deserialize<ExampleStruct>(request.JSON),
            _ => throw new Exception()
        };
        var fn = obj.FirstName + " foo";
        var ln = obj.LastName + " foo";


        return new FlexiResponse { JSON = JsonSerializer.Serialize(obj) };


    }

    [FlexiHandler("handlerAny", FlexiHandlerScope.Lifetime)]
    public FlexiResponse HandlerAny(FlexiRequest request)
    {
        var obj = request.PayloadCase switch
        {
            FlexiRequest.PayloadOneofCase.Any => request.Any.Unpack<ExampleMessage>(),
            _ => throw new Exception()
        };
        var fn = obj.FirstName + " foo";
        var ln = obj.LastName + " foo";


        return new FlexiResponse { JSON = JsonSerializer.Serialize(obj) };

    }



}
