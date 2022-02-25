using flexiservice;
using Google.Protobuf;
using OneOf;
using System.Text.Json;

namespace core.Tests;

[TestFixture]
public class FlexiHandlerBuilderTests
{




    [Test]
    public void Foo()
    {



        var builder = new FlexiHandlerBuilder();

        builder.AddType(typeof(Example));


        var val = builder.Build().First().Value;

        val.Delegate(new FlexiRequest());
    }


    [FlexiHandlerFixture(FlexiFixtureScope.LifetimePerScope)]
    private class Example
    {
        public interface IFooService
        {
            void Do();
        }

        public Example()
        {

        }

        [FlexiHandler("handler1")]
        public OneOf<object, IMessage> HandlerJson(OneOf<string, IMessage> json)
        {


            return "";

        }

        [FlexiHandler("handler2")]
        public OneOf<object, IMessage> HandlerJson2(OneOf<string, IMessage> json)
        {


            return "";

        }

        //[FlexiHandler("handler1", FlexiHandlerScope.Lifetime)]
        //public FlexiResponse HandlerFooWithParams(FlexiRequest request, IFooService svc)
        //{
        //    svc.Do();
        //    return new FlexiResponse();
        //}

    }
}
