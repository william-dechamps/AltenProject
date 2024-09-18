namespace AltenProject.Helpers;
using AltenProject.Services;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}
