using SDI.Dependencies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SDI
{
    public class InjectorFactory : IInjectorFactory
    {
        private Dictionary<Type, DependencyInfo> Dependencies = new Dictionary<Type, DependencyInfo>();

        private void AddDependency<REFERENCE, IMPLEMENTATION>(DependencyScope scope, IDependencyFactory<IMPLEMENTATION> factory)
            where IMPLEMENTATION : REFERENCE
        {
            if (Dependencies.ContainsKey(typeof(REFERENCE)))
            {
                throw new Exception($"Dependency '{typeof(REFERENCE).FullName}' already exists.");
            }
            Dependencies.Add(typeof(REFERENCE), new DependencyInfo(scope, factory));
        }

        public void AddSingleton<IMPLEMENTATION>()
        {
            AddSingleton<IMPLEMENTATION, IMPLEMENTATION>();
        }

        public void AddSingleton<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE
        {
            AddDependency<REFERENCE, IMPLEMENTATION>(DependencyScope.Singleton, new Inject<IMPLEMENTATION>());
        }

        public void AddScoped<IMPLEMENTATION>()
        {
            AddScoped<IMPLEMENTATION, IMPLEMENTATION>();
        }

        public void AddScoped<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE
        {
            AddDependency<REFERENCE, IMPLEMENTATION>(DependencyScope.Scoped, new Inject<IMPLEMENTATION>());
        }

        public void AddTransient<IMPLEMENTATION>()
        {
            AddTransient<IMPLEMENTATION, IMPLEMENTATION>();
        }

        public void AddTransient<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE
        {
            AddDependency<REFERENCE, IMPLEMENTATION>(DependencyScope.Transient, new Inject<IMPLEMENTATION>());
        }

        public IInjector GetInjector(IInjector? parent = null)
        {
            return new Injector(Dependencies, parent);
        }
    }
}
