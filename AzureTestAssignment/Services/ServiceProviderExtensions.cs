using AzureTestAssignment.Services.Interfaces;
using AzureTestAssignment.Services.Services;

namespace AzureTestAssignment.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            _ = services.AddTransient(typeof(IUploader), typeof(Uploader));
            _ = services.AddTransient(typeof(IBusinessLogic), typeof(BusinessLogic));
        }
    }
}
