using System;
using System.Collections.Generic;
using System.Text;

namespace SDI.Dependencies
{
    public abstract class OneTimeDependency<INSTANCE> : Dependency
    {
        private bool InstanceGenerated = false;
        private INSTANCE Instance { get; set; } = default;

        protected OneTimeDependency(DependencyScope instanceScope, IInjector injector)
            : base(instanceScope, injector) { }

        protected abstract INSTANCE CreateInstance();

        public override object? GetInstance()
        {
            if (!InstanceGenerated)
            {
                Instance = CreateInstance();
                InstanceGenerated = true;
            }
            return Instance;
        }
    }
}
