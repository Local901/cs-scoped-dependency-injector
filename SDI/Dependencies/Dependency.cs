using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public abstract class Dependency : IDependency
    {
        public DependencyScope InstanceScope { get; }
        protected IInjector Injector { get; }

        protected Dependency(DependencyScope scope, IInjector injector)
        {
            InstanceScope = scope;
            Injector = injector;
        }

        public abstract object? GetInstance();
    }
}
