using System.Reflection;

namespace core
{
    public struct FlexiHandlerSite : IDisposable
    {
        public object Instance;
        public FlexiHandlerDelegate Delegate;

        public void Dispose()
        {
            if (Instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    public class FlexiHandlerBuilder
    {
        private readonly IDictionary<string, FlexiHandlerSite> _delegateMap;
        private readonly HashSet<Assembly> _assemblies = new();
        private readonly HashSet<Type> _types = new();

        public FlexiHandlerBuilder()
        {
            _delegateMap = new Dictionary<string, FlexiHandlerSite>();
        }

        private static void ValidateMethod(MethodInfo method)
        {
            if (method.IsAbstract)
            {
                throw new ArgumentException("method cannot be abstract");
            }
            if (method.IsStatic)
            {
                // Static vs non static delegate sites
            }
            if (method.DeclaringType is null)
            {
                throw new ArgumentException("Declaring type cannot be null", nameof(method));
            }
        }

        private static FlexiHandlerDelegate GetDelegate(MethodInfo method, ref object? instance)
        {
            instance ??= Activator.CreateInstance(method.DeclaringType!);

            if (instance is null)
            {
                throw new Exception("instance cannot be null");
            }

            var target = Delegate.CreateDelegate(
                typeof(FlexiHandlerDelegate),
                instance,
                method) as FlexiHandlerDelegate ?? throw new InvalidProgramException($"Failed to create delegate from {method}");

            return target;
        }

        private object? GetSuitableInstance(FlexiFixtureScope scope, object? localCache)
        {
            return scope switch
            {
                FlexiFixtureScope.PerInstance => null,
                FlexiFixtureScope.LifetimePerScope => localCache,
                _ => throw new ArgumentException("invalid scope", nameof(scope))
            };
        }

        private void GenerateFromType(Type type)
        {
            var attribute = type.GetCustomAttribute<FlexiHandlerFixture>();

            if (attribute is not null)
            {
                GenerateFromType(type, attribute.Scope);
            }
        }

        private void GenerateFromType(Type type, FlexiFixtureScope scope)
        {
            object? instance = null;
            foreach (var method in type.GetMethods())
            {
                var attr = method.GetCustomAttribute<FlexiHandlerAttribute>();

                if (attr is not null)
                {
                    ValidateMethod(method);
                    instance = GetSuitableInstance(scope, instance);
                    var del = GetDelegate(method, ref instance);

                    _delegateMap.Add(attr.HandlerName, new() { Delegate = del, Instance = instance! });
                }
            }
        }

        public void AddAssembly(Assembly source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            _assemblies.Add(source);
        }

        public void AddType(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            _types.Add(type);
        }

        public FlexiHandlerCollection Build()
        {
            foreach (var asm in _assemblies)
            {
                foreach (Type t in asm.ExportedTypes)
                { 
                    GenerateFromType(t);
                }
            }
            foreach (var type in _types)
            {
                GenerateFromType(type);
            }
            return new(_delegateMap);
        }
    }
}
