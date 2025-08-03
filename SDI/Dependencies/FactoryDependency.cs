using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public class FactoryDependency<INSTANCE> : OneTimeDependency<INSTANCE>
    {
        private readonly Func<IInjector, INSTANCE> factory;

        public FactoryDependency(DependencyScope instanceScope, IInjector injector, Func<IInjector, INSTANCE> factory)
            : base(instanceScope, injector)
        {
            this.factory = factory;
        }

        protected override INSTANCE CreateInstance()
        {
            return factory(Injector);
        }
    }
}
