using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebPush.Client;

public partial class WebPushControl : IAsyncDisposable
{
    [Inject]
    public required IJSRuntime _JSRuntime { get; set; }
    [Parameter]
    public string WebPushPublicKey { get; set; } = string.Empty;

    WebPushJsInterop pushNotificationsJsInterop { get; set; }

    protected override async Task OnInitializedAsync()
    {
        pushNotificationsJsInterop = new WebPushJsInterop(_JSRuntime);
        await pushNotificationsJsInterop.ServiceWorkRegister();
    }

    public async Task<NotificationSubscription> RequestNotificationSubscriptionAsync()
    {
        var subscription = await pushNotificationsJsInterop.RequestSubscription(WebPushPublicKey);
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
