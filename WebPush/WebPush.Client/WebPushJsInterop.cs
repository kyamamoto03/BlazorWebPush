using Microsoft.JSInterop;

namespace WebPush.Client
{
    public class WebPushJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public WebPushJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/WebPush.Client/web-push.js").AsTask());
        }

        public async ValueTask ServiceWorkRegister()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("serviceWorkRegister", null);
        }

        public async ValueTask<NotificationSubscription> RequestSubscription(string publicKey)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync< NotificationSubscription>("requestSubscription", publicKey);
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
