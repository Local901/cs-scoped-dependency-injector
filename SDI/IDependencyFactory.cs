using System;
using System.Collections.Generic;
using System.Text;

namespace SDI
{
    public interface IDependencyFactory
    {
        public IDependency CreateDependency(DependencyScope instanceScope, IInjector injector);
    }

    public interface IDependencyFactory<INSTANCE> : IDependencyFactory
    {
        public new IDependency CreateDependency(DependencyScope instanceScope, IInjector injector);
    }
}
