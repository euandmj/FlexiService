using Google.Protobuf;
using OneOf;

namespace core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class FlexiHandlerAttribute : Attribute
{
    public string HandlerName { get; }
    //public FlexiHandlerScope Scope { get; }

    public FlexiHandlerAttribute(string handlerName)
    {
        HandlerName = handlerName;
        //Scope = scope;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class FlexiHandlerFixture : Attribute
{
    public FlexiFixtureScope Scope { get; }
    public FlexiHandlerFixture(FlexiFixtureScope scope = FlexiFixtureScope.PerInstance) => Scope = scope;
}

public enum FlexiHandlerScope
{

}

public enum FlexiFixtureScope
{
    /// <summary>
    /// Instances should be reused within the scope of the type
    /// </summary>
    LifetimePerScope,
    /// <summary>
    /// Instances should be newed up per delgate handler.
    /// </summary>
    PerInstance
}

public delegate OneOf<object, IMessage> FlexiHandlerDelegate(OneOf<string, IMessage> request);
//public delegate FlexiResponse FlexiHandlerDelegate(FlexiRequest request);


