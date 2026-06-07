namespace Application;

public static class ApplicationRegisterExtension
{
    public static void ApplicationRegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<ITypeProductRepository, TypeProductsRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ITypeService, TypeServices>();
        services.AddTransient<ITypeProductService, TypeProductService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITokenService, TokenService>();
    }
}
