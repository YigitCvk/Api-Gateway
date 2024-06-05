using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public static class ServiceDiscoveryExtensions
    {
        public static void AddConsul(this IServiceCollection services, ServiceConfig serviceConfig)
        {
            if (serviceConfig == null)
            {
                throw new ArgumentNullException(nameof(serviceConfig));
            }

            var consulClient = new ConsulClient(config =>
            {
                config.Address = serviceConfig.DiscoveryAddress;
            });

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
        }
    }
}
