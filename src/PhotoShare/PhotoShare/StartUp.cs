using Microsoft.Extensions.DependencyInjection;
using System;

namespace PhotoShare
{
    public static class StartUp
    {
        public static void Init(Action<IServiceCollection> nativeConfigSevice)
        {
            var service = new ServiceCollection();

            nativeConfigSevice(service);

            App.ServiceProvider = service.BuildServiceProvider();
        }

        
    }
}
