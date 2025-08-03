using System;
using System.Collections.Generic;
using System.Text;

namespace SDI
{
    public interface IInjector
    {
        public INSTANCE GetInstance<INSTANCE>()
            where INSTANCE : class;
        public object? GetInstance(Type type);

        public INSTANCE CreateInstance<INSTANCE>();
        public object? CreateInstance(Type type);

        public bool IsDependency<DEPENDENCY>();
        public bool IsDependency(Type type);

        public IDependency? GetDependency<DEPENDENCY>();
        public IDependency? GetDependency(Type type);

        public IDependency? GetSingletonDependency(Type type);
    }
}
