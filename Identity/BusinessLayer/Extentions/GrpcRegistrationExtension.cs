using BusinessLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace BusinessLayer.Extentions
{
    public static class GrpcRegistrationExtension
    {
        public static void ReristerRrpcService(this IServiceCollection services)
        {
            services.AddCodeFirstGrpc();
        }

        public static void UseGrpcService(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserNicknameService>();
            });
        }
    }
}
