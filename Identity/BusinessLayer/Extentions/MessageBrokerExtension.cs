using MassTransit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.DTOs;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Extensions
{
    public static class MessageBrokerExtension
    {
        public static void ConfigureMassTransit(
            this IServiceCollection services,
            ConfigurationManager config
        )
        {
            services.Configure<RabbitMQSettings>(config.GetSection("RabbitMQ"));

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq(
                    (busRegistrationContext, busConfigurator) =>
                    {
                        var settings = busRegistrationContext
                            .GetRequiredService<IOptions<RabbitMQSettings>>()
                            .Value;

                        busConfigurator.Host(
                            new Uri(settings.Host),
                            hostConfigurator =>
                            {
                                hostConfigurator.Username(settings.Username);
                                hostConfigurator.Password(settings.Password);
                            }
                        );
                    }
                );
            });
        }
    }
}
