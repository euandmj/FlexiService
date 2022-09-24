using System.Reflection;

namespace core;

internal interface IFlexiHandlerBuilder
{
    public void Add(Assembly assembly);
    public void Add(Type type);
    public HandlerSiteCollection Build();
}