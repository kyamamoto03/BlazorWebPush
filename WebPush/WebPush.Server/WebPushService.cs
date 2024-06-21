using WebPush;

namespace PushService.Server;

public interface IPushNotification
{
    ValueTask PushAsync(string endPoint, string p256dh, string auth, string message);
}

public class WebPushService : IPushNotification
{
    private WebPushConfig _pushServiceConfig { get; init; }

    public WebPushService(WebPushConfig pushServiceConfig)
    {
        _pushServiceConfig = pushServiceConfig;
    }

    public async ValueTask PushAsync(string endPoint, string p256dh, string auth, string message)
    {
        // For a real application, generate your own
        var publicKey = _pushServiceConfig.PublicKey;
        var privateKey = _pushServiceConfig.PrivateKey;

        var pushSubscription = new PushSubscription(endPoint, p256dh, auth);

        var vapidDetails = new VapidDetails("mailto:<someone@example.com>", publicKey, privateKey);
        var webPushClient = new WebPushClient();
        try
        {
            var payload = System.Text.Json.JsonSerializer.Serialize(new { message });
            await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error sending push notification: " + ex.Message);
        }
    }
}

