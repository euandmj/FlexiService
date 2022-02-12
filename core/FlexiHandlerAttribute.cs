using flexiservice;

namespace core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class FlexiHandlerAttribute : Attribute
{
    public string HandlerName { get; }
    public FlexiHandlerScope Scope { get; }

    public FlexiHandlerAttribute(
        string handlerName,
        FlexiHandlerScope scope)
    {
        HandlerName = handlerName;
        Scope = scope;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class FlexiHandlerFixture : Attribute
{
}

public enum FlexiHandlerScope
{
    Singleton,
    Lifetime
}

//public delegate FlexiResponse FlexiHandlerDelegate<T>(OneOf<string, T> request);
public delegate FlexiResponse FlexiHandlerDelegate(FlexiRequest request);


