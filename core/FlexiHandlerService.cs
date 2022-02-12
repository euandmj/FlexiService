using flexiservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core;

public class FlexiHandlerService
{
    private readonly FlexiHandlerBuilder _builder = new();





    public FlexiHandlerService(
        IEnumerable<Assembly> assemblies,
        IEnumerable<Type> types)
    {

        

    }



    public T? Get<T>(string target, FlexiRequest requestData)
    {
        return default;
        try
        {
            if(_builder.DelegateMap.TryGetValue(target, out var func))
            {
            }
        }
        catch
        {
            return default;
        }
    }



}
