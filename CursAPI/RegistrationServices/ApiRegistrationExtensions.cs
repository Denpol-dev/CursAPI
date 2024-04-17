using CursAPI.Enities;
using CursAPI.Services.ClientServices;
using CursAPI.Services.Identity;
using CursAPI.Services.UserServices;

namespace CursAPI.RegistrationServices
{
    public static class ApiRegistrationExtensions
    {
        /// <summary>
        /// Регистрация сервисов приложения
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
