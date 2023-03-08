using Api;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//services.AddTransient<IIdGenerator>(sp => 
//    new IdGenerator(sp.GetRequiredService<ILogger<IdGenerator>>()));

//services.AddTransient(typeof(IService), typeof(Service));

//services.AddSingleton<IGetProductsUseCase>(
//    sp => new GetProductsUseCase(
//        sp.GetRequiredService<ILogger<GetProductsUseCase>>(),
//        sp.GetRequiredService<IService>())
//    );

// services.AddTransient<IIdGenerator, IdGenerator>();

var descriptor = new ServiceDescriptor(
    typeof(IIdGenerator),
    typeof(IdGenerator),
    ServiceLifetime.Transient
);

services.Add(descriptor);

// services.AddTransient<IService, Service>();
var serviceDescriptor = new ServiceDescriptor(
    typeof(IService),
    typeof(Service),
    ServiceLifetime.Transient
);

services.Add(serviceDescriptor);

services.AddTransient<IGetProductsUseCase, GetProductsUseCase>();

services.AddTransient(typeof(INameService<>), typeof(NameService<>));
services.AddTransient<
    INameService<ProductsController>, 
    DetailedNameService<ProductsController>>();

//services.AddTransient<
//    INameService<GetProductsUseCase>,
//    NameService<GetProductsUseCase>>();

services.AddTransient<LogFilter>();
services.AddSingleton<IServiceSingleton, ServiceSingleton>();
services.AddScoped<IServiceScoped, ServiceScoped>();

services.AddScoped<IProcessPaymentUseCase, ProcessPaymentUseCase>();

services.AddHostedService<MyBackgroundService>();

var app = builder.Build();

app.UseMiddleware<MyMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
