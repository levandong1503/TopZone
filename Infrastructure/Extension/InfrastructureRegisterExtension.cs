using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension;

public static class InfrastructureRegisterExtension
{
    public static void InfrastructureRegisterService(this IServiceCollection services)
    {
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}
