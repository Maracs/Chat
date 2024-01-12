namespace ApiGateway.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(
            this IServiceCollection services,
            ConfigurationManager config
        )
        {
           
            var apiGateway = config["AllowedOrigins:ApiGateway"];
            var chat = config["AllowedOrigins:Chat"];
            var group = config["AllowedOrigins:Group"];
            var identity = config["AllowedOrigins:Identity"];
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins( apiGateway, chat, group, identity)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
