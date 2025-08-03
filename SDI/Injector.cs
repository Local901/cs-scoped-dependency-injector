using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDI
{
    public class Injector : IInjector
    {
        private readonly IReadOnlyDictionary<Type, DependencyInfo> Dependencies;
        private readonly Dictionary<Type, IDependency> Instances = new Dictionary<Type, IDependency>();
        private readonly IInjector? Parent;

        public Injector(IReadOnlyDictionary<Type, DependencyInfo> dependencies, IInjector? parent)
        {
            Dependencies = dependencies;
            Parent = parent;
        }

        public INSTANCE CreateInstance<INSTANCE>()
        {
            Type type = typeof(INSTANCE);
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    var parameters = constructor.GetParameters()
                        .Select((parameter) => GetInstance(parameter.ParameterType))
                        .ToArray();
                    var instance = constructor.Invoke(parameters);
                    if (instance is INSTANCE result)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            throw new Exception($"No instance could be created of '{nameof(INSTANCE)}'.");
        }

        public INSTANCE GetInstance<INSTANCE>() where INSTANCE : class
        {
            if (this is INSTANCE factory)
            {
                return factory;
            }
            IDependency? dependency = GetDependency<INSTANCE>();
            if (dependency != null)
            {
                return dependency.GetInstance() as INSTANCE;
            }
            throw new Exception($"No instance of '{nameof(INSTANCE)}' found.");
        }

        public object? CreateInstance(Type type)
        {
            var method = typeof(Injector)
                .GetMethods()
                .FirstOrDefault((method) =>
                    method.Name == "CreateInstance" &&
                    method.IsGenericMethod &&
                    method.GetParameters().Length == 0
                );
            if (method == null)
            {
                throw new Exception("Whoops this shouldn't happen: could not find Generic CreateInstance function on Injector.");
            }
            return method
                .MakeGenericMethod(type)
                .Invoke(this, null);
        }

        public object? GetInstance(Type type)
        {
            var method = typeof(Injector)
                .GetMethods()
                .FirstOrDefault((method) =>
                    method.Name == "GetInstance" &&
                    method.IsGenericMethod &&
                    method.GetParameters().Length == 0
                );
            if (method == null)
            {
                throw new Exception("Whoops this shouldn't happen: could not find Generic GetInstance function on Injector.");
            }
            return method
                .MakeGenericMethod(type)
                .Invoke(this, null);
        }

        public bool IsDependency<DEPENDENCY>()
        {
            return IsDependency(typeof(DEPENDENCY));
        }

        public bool IsDependency(Type type)
        {
            if (Dependencies.ContainsKey(type))
            {
                return true;
            } else if (Parent != null)
            {
                return Parent.IsDependency(type);
            }
            return false;
        }

        public IDependency? GetDependency<DEPENDENCY>()
        {
            return GetDependency(typeof(DEPENDENCY));
        }

        public IDependency? GetDependency(Type type)
        {
            DependencyInfo? optionalDependencyInfo = Dependencies.GetValueOrDefault(type);

            // - Dependency is not known in this Injector.
            if (!optionalDependencyInfo.HasValue)
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.GetDependency(type);
            }

            // - Dependency is known
            DependencyInfo dependencyInfo = optionalDependencyInfo.Value;

            // Instance already saved?
            if (Instances.ContainsKey(type))
            {
                return Instances[type];
            }

            // Singleton has to check parent Injector
            if (dependencyInfo.Scope == DependencyScope.Singleton)
            {
                return GetSingletonDependency(type);
            }

            // Make dependency
            var dependency = dependencyInfo.CreateDependency(this);

            if (dependencyInfo.Scope != DependencyScope.Transient)
            {
                Instances.Add(type, dependency);
            }
            return dependency;
        }

        public IDependency? GetSingletonDependency(Type type)
        {
            if (Parent != null)
            {
                var result = Parent.GetSingletonDependency(type);
                if (result != null)
                {
                    return result;
                }
            }

            DependencyInfo? optionalDependencyInfo = Dependencies.GetValueOrDefault(type);

            if (!optionalDependencyInfo.HasValue)
            {
                return null;
            }

            // Make dependency
            var dependency = optionalDependencyInfo.Value.CreateDependency(this);
            Instances.Add(type, dependency);
            return dependency;
        }
    }
}
