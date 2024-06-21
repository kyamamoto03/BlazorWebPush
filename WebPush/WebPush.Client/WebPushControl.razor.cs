using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebPush.Client;

public partial class WebPushControl : IAsyncDisposable
{
    [Inject]
    public required IJSRuntime _JSRuntime { get; set; }

    WebPushJsInterop pushNotificationsJsInterop { get; set; }

    protected override async Task OnInitializedAsync()
    {
        pushNotificationsJsInterop = new WebPushJsInterop(_JSRuntime);
        await pushNotificationsJsInterop.ServiceWorkRegister();
    }

    public async Task<NotificationSubscription> RequestNotificationSubscriptionAsync()
    {
        var subscription = await pushNotificationsJsInterop.RequestSubscription("BD4ldrURU9tPMSWtq-iqG4D6i2m4_IpbvNEsmJxakVgbSV-fxKBhJHouPnkPwRsDI4Yu_gg745t7OjYWLBwAEfA");
        // subscriptionがnullじゃない場合Consoleに出力
        if (subscription != null)
        {
            Console.WriteLine(subscription.Url);
            Console.WriteLine(subscription.P256dh);
            Console.WriteLine(subscription.Auth);

        }
        return subscription;
    }

    public async ValueTask DisposeAsync()
    {
        if (pushNotificationsJsInterop != null)
        {
            await pushNotificationsJsInterop.DisposeAsync();
        }
    }
}
