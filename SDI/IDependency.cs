using System;
using System.Collections.Generic;
using System.Text;

namespace SDI
{
    public interface IDependency
    {
        public DependencyScope InstanceScope { get; }
        public object? GetInstance();
    }
}
