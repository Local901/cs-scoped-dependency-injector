using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public class RecursiveDependency<INSTANCE> : OneTimeDependency<INSTANCE>
    {
        public RecursiveDependency(DependencyScope instanceScope, IInjector injector)
            : base(instanceScope, injector) { }

        protected override INSTANCE CreateInstance()
        {
            return Injector.CreateInstance<INSTANCE>();
        }
    }
}
