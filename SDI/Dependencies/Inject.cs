using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public class Inject<INSTANCE> : IDependencyFactory<INSTANCE>
    {
        public IDependency CreateDependency(DependencyScope instanceScope, IInjector injector)
        {
            return new RecursiveDependency<INSTANCE>(instanceScope, injector);
        }
    }
}
