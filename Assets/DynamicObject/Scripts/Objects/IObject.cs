using System;

namespace DynamicObjects
{
    public interface IObject
    {
        IObject Root { get; }
        
        ///Properties 
        T GetProperty<T>(string name);

        Func<T> GetPropertyPtr<T>(string name);

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

// void DefineProperty<T>(string name, Func<T> provider);
//
// void RemoveProperty<T>(string name);
//
// void DefineMethod(string name, Action provider);
//
// void DefineMethod<T>(string name, Action<T> provider);
//
// void DefineMethod<R>(string name, Func<R> provider);
//
// void DefineMethod<T, R>(string name, Func<T, R> provider);
//
// void DefineEvent(string name);
//
// void DefineEvent<T>(string name);
//
// void InvokeEvent(string name);
//
// void InvokeEvent<T>(string name, T data);