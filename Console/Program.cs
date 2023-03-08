using Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

// services.AddLogging(builder => builder.AddConsole());
services.AddTransient<IIdGenerator, IdGenerator>();
services.AddTransient<IService, Service>();
// services.AddTransient<IApp, App>();

var provider = services.BuildServiceProvider();
var scope = provider.CreateScope();

var app = provider.GetService<IApp>();

app?.Run();