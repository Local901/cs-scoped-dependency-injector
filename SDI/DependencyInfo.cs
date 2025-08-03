using System;
using System.Collections.Generic;
using System.Text;

namespace SDI
{
    public struct DependencyInfo
    {
        public readonly DependencyScope Scope;
        private readonly IDependencyFactory _factory;

        public DependencyInfo(DependencyScope scope, IDependencyFactory factory)
        {
            Scope = scope;
            _factory = factory;
        }

        public IDependency CreateDependency(IInjector injector)
        {
            return _factory.CreateDependency(Scope, injector);
        }
    }
}
