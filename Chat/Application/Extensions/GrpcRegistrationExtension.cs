using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;


namespace Application.Extensions
{
    public static class GrpcRegistrationExtension
    {
        public static void RegisterGrpcClient(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            var endpoint = config["IdentityConfig:EndPoint"];
            var channel = GrpcChannel.ForAddress(endpoint);

            services.AddSingleton(channel);

            services.AddScoped<IUserNicknameService>(provider =>
            {
                var client = channel.CreateGrpcService<IUserNicknameService>();

                return client;
            });
        }
    }
}
