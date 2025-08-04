# Scoped Dependency Injector

```cs
var injectorFactory1 = new Injector();
var injectorFactory2 = new Injector();

injectorFactory1.AddSingleton<ISingleton, Singleton>();
injectorFactory1.AddScoped<IScoped, Scoped>();
injectorFactory1.AddSingleton<IScopeSpecific, ScopeSpecific>();

injectorFactory2.AddSingleton<ISingleton, Singleton>();
injectorFactory2.AddScoped<IScoped, Scoped>();

var injector1 = injectorFactory1.GetInjector();
var injector2 = injectorFactory2.GetInjector(injector1);

injector2.GetInstance<IScoped>(); // Returns instance of Scoped from injector2.
injector2.GetInstance<ISingleton>(); // Returns instance of Singleton from injector1.
injector2.GetInstance<IScopeSpecific>(); // Returns instance from injector1.
```
