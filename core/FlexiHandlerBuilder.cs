using flexiservice;
using System.Reflection;

namespace core
{
    public class FlexiHandlerBuilder
    {
        public IDictionary<string, FlexiHandlerDelegate> DelegateMap { get; }


        public FlexiHandlerBuilder()
        {
            DelegateMap = new Dictionary<string, FlexiHandlerDelegate>();
        }


        private static void ValidateMethod(MethodInfo method)
        {
            if (method.ReturnType != typeof(FlexiResponse))
            {
                throw new InvalidProgramException(
                    "Return value must be of type FlexiMessage");
            }
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                throw new InvalidProgramException(
                    "FlexiHandler must have one param");
            }
            if (parameters[0].ParameterType != typeof(FlexiRequest))
            {
                throw new InvalidProgramException(
                    "First parameter must be of type FlexiMessage");
            }
        }

        private static FlexiHandlerDelegate GetDelegate(MethodInfo method)
        {
            if (method.DeclaringType is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            return Delegate.CreateDelegate(
                typeof(FlexiHandlerDelegate),
                Activator.CreateInstance(method.DeclaringType),
                method) as FlexiHandlerDelegate ?? throw new InvalidProgramException($"Failed to create delegate from {method}");
        }

        private void GenerateFromType(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                var attr = method.GetCustomAttribute<FlexiHandlerAttribute>();

                if (attr is not null)
                {
                    // assert the return type is invalid. 
                    ValidateMethod(method);

                    DelegateMap.Add(attr.HandlerName, GetDelegate(method));
                }
            }
        }

        public void Generate(Assembly source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            foreach (Type t in source.ExportedTypes
                .Where(t => t.GetCustomAttribute<FlexiHandlerFixture>() is not null))
            {
                GenerateFromType(t);
            }
        }

        public void Generate(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            GenerateFromType(type);
        }
    }
}
