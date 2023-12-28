using Application.Consumers;
using Application.Dtos;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application.Extensions
{
    public static class MessageBrokerExtension
    {
        public static void ConfigureMassTransit(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.Configure<RabbitMQSettings>(config.GetSection("RabbitMQ"));

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<UserDeletedConsumer>();

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

                        busConfigurator.ConfigureEndpoints(busRegistrationContext);
                    }
                );
            });
        }
    }
}
