using System;
using System.Collections.Generic;
using System.Text;

namespace SDI
{
    public interface IInjectorFactory
    {
        public void AddSingleton<IMPLEMENTATION>();
        public void AddSingleton<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE;
        public void AddScoped<IMPLEMENTATION>();
        public void AddScoped<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE;
        public void AddTransient<IMPLEMENTATION>();
        public void AddTransient<REFERENCE, IMPLEMENTATION>()
            where IMPLEMENTATION : REFERENCE;

        public IInjector GetInjector(IInjector? parent = null);
    }
}
