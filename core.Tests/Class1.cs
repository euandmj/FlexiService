using flexiservice;

namespace core.Tests;

[TestFixture]
public class FlexiHandlerBuilderTests
{




    [Test]
    public void Foo()
    {

        var builder = new FlexiHandlerBuilder();

        builder.Generate(typeof(Example));


        var val = builder.DelegateMap.First().Value;

        val(new FlexiRequest());
    }


    [FlexiHandlerFixture]
    private class Example
    {
        public interface IFooService
        {
            void Do();
        }

        [FlexiHandler("handler1", FlexiHandlerScope.Lifetime)]
        public FlexiResponse HandlerFoo(FlexiRequest request)
        {
            return new FlexiResponse();
        }

        [FlexiHandler("handler1", FlexiHandlerScope.Lifetime)]
        public FlexiResponse HandlerFooWithParams(FlexiRequest request, IFooService svc)
        {
            svc.Do();
            return new FlexiResponse();
        }

    }
}
