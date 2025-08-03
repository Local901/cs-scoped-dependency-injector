using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public class TransientDependency<INSTANCE> : IDependency
    {
        public DependencyScope InstanceScope => DependencyScope.Transient;
        private readonly Func<IInjector, INSTANCE> Factory;

        public TransientDependency(Func<IInjector, INSTANCE> factory)
        {
            Factory = factory;
        }

        public object? GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}
