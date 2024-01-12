using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer.Extentions
{
    public static class KestrelExtension
    {
        public static void ConfigureKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(options =>
             {
                 var grpcPort = builder.Configuration.GetValue("GRPC_PORT", 5101);
                 var httpPort = builder.Configuration.GetValue("PORT", 5001);

                 options.Listen(IPAddress.Any, httpPort, listenOptions =>
                 {
                     listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                     listenOptions.UseHttps();
                 });

                 options.Listen(IPAddress.Any, grpcPort, listenOptions =>
                 {
                     listenOptions.Protocols = HttpProtocols.Http2;
                 });
             });
        }
    }
}
