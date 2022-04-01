using System;

namespace DynamicObjects
{
    public interface IObject
    {
        IObject Root { get; }
        
        ///Properties 
        T GetProperty<T>(string name);

        bool TryGetProperty<T>(string name, out T property);

        bool TryGetPropertyPtr<T>(string name, out Func<T> provider);

        ///Methods 
        void CallMethod(string name);

        void CallMethod<T>(string name, T data);

        R CallMethod<R>(string name);

        R CallMethod<T, R>(string name, T data);

        bool TryGetMethodPtr(string name, out Action provider);

        bool TryGetMethodPtr<T>(string name, out Action<T> provider);

        bool TryGetMethodPtr<R>(string name, out Func<R> provider);

        bool TryGetMethodPtr<T, R>(string name, out Func<T, R> provider);
        
        ///Events 
        void AddListener(string name, Action callback);

        void AddListener<T>(string name, Action<T> callback);

        void RemoveListener(string name, Action callback);

        void RemoveListener<T>(string name, Action<T> callback);
    }
}