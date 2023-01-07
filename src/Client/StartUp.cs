using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Radzen;
using RateMyManagementWASM.Client.Configuration;
using RateMyManagementWASM.Client.Configuration.Handlers;
using RateMyManagementWASM.Client.IServices;
using RateMyManagementWASM.Client.Services;

namespace RateMyManagementWASM.Client
{
    public class StartUp
    {
        public static void ConfigureServices(IServiceCollection services, string httpBaseAddress)
        {
            ConfigureHttpClients(services, httpBaseAddress);
            ConfigureAuthorization(services);
            ConfigureAuthorizationHandlers(services);
            ConfigureProjectServices(services);

            services.AddBlazoredLocalStorage();
            services.AddScoped<ContextMenuService>();
        }

        private static void ConfigureProjectServices(IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IImageService, ImgbbService>();
            services.AddScoped<HttpLocationService>();
            services.AddScoped<HttpCompanyService>();
            services.AddScoped<HttpLocationReviewService>();
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorizationCore(AuthorizationOptionsConfigurer.Configure);
            services.AddApiAuthorization();
        }

        private static void ConfigureHttpClients(IServiceCollection services, string httpBaseAddress)
        {
            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddHttpClient("RateMyManagementWASM.ServerAPI",
                    client => client.BaseAddress = new Uri(httpBaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            services.AddHttpClient("public", client => client.BaseAddress = new Uri(httpBaseAddress));
            services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("RateMyManagementWASM.ServerAPI"));
        }

        private static void ConfigureAuthorizationHandlers(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, LocationManagerHandler>();
            services.AddSingleton<IAuthorizationHandler, CompanyManagerHandler>();

        }
    }
}