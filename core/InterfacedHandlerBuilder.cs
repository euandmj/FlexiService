using System.Reflection;

namespace core;

internal class InterfacedHandlerBuilder : IFlexiHandlerBuilder
{
    private readonly IDictionary<string, IFlexiHandler> _anyMap;
    // private readonly IDictionary<string, IFlexiHandlerJson<> _jsonMap;
    private readonly HashSet<Assembly> _assemblies = new();
    private readonly HashSet<Type> _types = new();

    void f(Type type)
    {
        // TODO!
        // CreateInstance Resolved Args
        // Resolve from cache (scoped)
        var instance = Activator.CreateInstance(type) as IFlexiHandler;
        if (instance is not null)
        {
            _anyMap[instance.HandlerName] = instance;
        }
    }

    public void Add(Assembly assembly)
    {
        throw new NotImplementedException();
    }

    public void Add(Type type)
    {
        throw new NotImplementedException();
    }

    public HandlerSiteCollection Build()
    {
        throw new NotImplementedException();
    }
}
