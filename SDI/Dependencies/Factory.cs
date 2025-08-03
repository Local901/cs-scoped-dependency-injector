using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public class Factory<INSTANCE> : IDependencyFactory<INSTANCE>
    {
        private readonly Func<IInjector, INSTANCE> factory;

        public Factory(Func<IInjector, INSTANCE> factory)
        {
            this.factory = factory;
        }

        public IDependency CreateDependency(DependencyScope instanceScope, IInjector injector)
        {
            return new FactoryDependency<INSTANCE>(instanceScope, injector, CreateInstance);
        }

        private INSTANCE CreateInstance(IInjector injector)
        {
            return factory(injector);
        }
    }
}
