using Application.Interface;
using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;

namespace Application.extension;

public static class ApplicationRegisterExtension
{
    public static void ApplicationRegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<ITypeProductRepository, TypeProductsRepository>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ITypeService, TypeServices>();
        services.AddTransient<ITypeProductService, TypeProductService>() ;
    }
}
