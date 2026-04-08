using Application.Interface;
using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.extension;

public static class ApplicationRegisterExtension
{
    public static void ApplicationRegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<ITypeProductRepository, TypeProductsRepository>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ITypeService, TypeServices>();
        services.AddTransient<ITypeProductService, TypeProductService>();
    }
}
