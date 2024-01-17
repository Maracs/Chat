public static class HttpClientConfiguration
{
    public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration config)
    {
        string endpoint = config["IdentityConfig:Endpoint"];

        services.AddHttpClient(
            "TeamHubClient",
            client =>
            {
                client.BaseAddress = new Uri(endpoint);
            }
        );
    }
}
