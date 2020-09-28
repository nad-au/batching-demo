using System;
using Autofac;
using ServiceStack.Configuration;

namespace BatchingDemo
{
    public class AutofacIocAdapter : IContainerAdapter
    {
        private readonly IContainer container;

        public AutofacIocAdapter(IContainer container) => 
            this.container = container;

        public T TryResolve<T>()
        {
            if (container.TryResolve<ILifetimeScope>(out var scope) &&
                scope.TryResolve(typeof(T), out var scopeComponent))
                return (T)scopeComponent;

            if (container.TryResolve(typeof(T), out var component))
                return (T)component;

            return default;
        }

        public T Resolve<T>()
        {
            var ret = TryResolve<T>();
            return !ret.Equals(default)
                ? ret
                : throw new Exception($"Error trying to resolve '{typeof(T).Name}'");
        }
    }

}