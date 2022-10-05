using Google.Protobuf;
using Any = Google.Protobuf.WellKnownTypes.Any;
using OneOf;

namespace core
{
    public struct FlexiHandlerSite : IDisposable
    {
        public object Instance;
        public Func<OneOf<string, Any>, OneOf<object, IMessage>> Delegate;

        public void Dispose()
        {
            if (Instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public FlexiHandlerSite(object instance, FlexiHandlerDelegateAny handler)
        {
            ArgumentNullException.ThrowIfNull(instance);
            ArgumentNullException.ThrowIfNull(handler);

            Instance = instance;
            Delegate =  (x) => 
            {
                var xx = handler(x.AsT1);
                return xx; // TODO!
            };
        }

        public FlexiHandlerSite(object instance, FlexiHandlerDelegateJSON handler)
        {
            ArgumentNullException.ThrowIfNull(instance);
            ArgumentNullException.ThrowIfNull(handler);

            Instance = instance;
            Delegate = (x) => 
            {
                var xx = handler(x.AsT0);
                return xx;
            };
        }
    }
}
