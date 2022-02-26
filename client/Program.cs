// See https://aka.ms/new-console-template for more information
using flexiservice;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using static System.Console;

Console.WriteLine("Hello, World!");


var client = new FlexiService.FlexiServiceClient(GrpcChannel.ForAddress("https://localhost:7036"));



ReadKey();


//await client.SendMessageAsync(new FlexiRequest
//{
//    JSON = System.Text.Json.JsonSerializer.Serialize<ExampleStruct>(new ExampleStruct { FirstName = "bob", LastName = "ross" }),
//    Target = "handlerJson"
//});

await client.SendMessageAsync(new FlexiRequest
{
    Target = "handlerAny",
    Any = Any.Pack(new ExampleMessage { FirstName = "bob", LastName = "ross" })
});




public class ExampleStruct
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}