using System.Collections;

namespace core;

public class HandlerSiteCollection : IReadOnlyCollection<KeyValuePair<string, FlexiHandlerSite>>, IEnumerable<KeyValuePair<string, FlexiHandlerSite>>, IReadOnlyDictionary<string, FlexiHandlerSite>
{
    private readonly IDictionary<string, FlexiHandlerSite> _handlers;

    internal HandlerSiteCollection(IDictionary<string, FlexiHandlerSite> from)
    {
        _handlers = from;
    }

    public int Count => _handlers.Count;

    public IEnumerable<string> Keys => _handlers.Keys;

    public IEnumerable<FlexiHandlerSite> Values => _handlers.Values;

    public FlexiHandlerSite this[string key] => _handlers[key];

    public bool Contains(KeyValuePair<string, FlexiHandlerSite> item)
    {
        return _handlers.Contains(item);
    }

    public IEnumerator<KeyValuePair<string, FlexiHandlerSite>> GetEnumerator()
    {
        return _handlers.GetEnumerator();
    }

    public bool TryGetValue(string key, out FlexiHandlerSite value)
    {
        return _handlers.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_handlers).GetEnumerator();
    }

    public bool ContainsKey(string key)
    {
        return _handlers.ContainsKey(key);
    }
}
