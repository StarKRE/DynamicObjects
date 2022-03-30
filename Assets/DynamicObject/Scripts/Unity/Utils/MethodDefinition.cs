#if UNITY_EDITOR
using System;

namespace DynamicObjects
{
    public readonly struct MethodDefinition
    {
        public readonly MethodName name;

        private readonly string text;

        private MethodDefinition(MethodName name, string text)
        {
            this.name = name;
            this.text = text;
        }

        public override string ToString()
        {
            return this.text;
        }

        public static MethodDefinition Create(MethodName name)
        {
            var text = $"{name}()";
            return new MethodDefinition(name, text);
        }

        public static MethodDefinition Create<T>(MethodName name, Action<T> action)
        {
            var type = typeof(T);
            var text = $"{name}({TypeUtils.PrettyName(type)})";
            return new MethodDefinition(name, text);
        }

        public static MethodDefinition Create<R>(MethodName name, Func<R> func)
        {
            var type = typeof(R);
            var text = $"{name}() : {TypeUtils.PrettyName(type)}";
            return new MethodDefinition(name, text);
        }

        public static MethodDefinition Create<T, R>(MethodName name, Func<T, R> func)
        {
            var paramType = typeof(T);
            var resultType = typeof(R);
            var text = $"{name}({TypeUtils.PrettyName(paramType)}) : {TypeUtils.PrettyName(resultType)}";
            return new MethodDefinition(name, text);
        }
    }
}
#endif