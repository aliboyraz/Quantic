using System.Collections.Generic;
using Quantic.Core;

namespace Quantic.FeatureManagement
{
    public static class RequestContextExtension
    {
        // it should be part of core
        public static string GetHeaderValue(this RequestContext context, string key)
        {            
            context.Headers.TryGetValue(key, out string value);
            return value;
        }

        public static bool Add(this RequestContext context, string key, string value)
        {
            return context.Headers.TryAdd(key, value);
        }        
    }
}