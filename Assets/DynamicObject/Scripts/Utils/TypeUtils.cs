using System;
using System.Linq;

namespace DynamicObjects
{
    internal static class TypeUtils
    {
        public static string PrettyName(System.Type type)
        {
            if (!type.IsGenericType)
            {
                return BaseName(type);
            }

            var genericArguments = type.GetGenericArguments()
                .Select(PrettyName)
                .Aggregate((x1, x2) => $"{x1}, {x2}");
            
            var name = BaseName(type);
            return $"{name.Substring(0, name.IndexOf("`", StringComparison.Ordinal))}" +
                   $"<{genericArguments}>";
        }

        private static string BaseName(System.Type type)
        {
            var typeName = type.Name;
            return typeName switch
            {
                "Boolean" => "bool",
                "Int32" => "int",
                "String" => "string",
                "Single" => "float",
                _ => typeName
            };
        }
    }
}