using Microsoft.Extensions.DependencyInjection;

namespace PushService.Server;

public static class WebPushExtension
{
    public static IServiceCollection AddWebPush(this IServiceCollection serviceCollection,Action<WebPushConfig> action)
    {
        serviceCollection.AddScoped<IPushNotification, WebPushService>();

        WebPushConfig pushServiceConfig = new();
        action(pushServiceConfig);
        serviceCollection.AddSingleton(pushServiceConfig);

        return serviceCollection;
    }
}
