using System.Text.Json;
using System.Text;

namespace BlazorWebPush.Client;


public interface IWebPushUsecase
{
    Task SubscribeAsync(string endPoint, string p256dh, string auth);
}

public class WebPushUsecase : IWebPushUsecase
{
    private HttpClient httpClient { get; init; }

    public WebPushUsecase(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task SubscribeAsync(string endPoint, string p256dh, string auth)
    {
        await httpClient.PostAsync("api/WebPush/Subscribe",
            new StringContent(JsonSerializer.Serialize(new { endPoint, p256dh, auth }),
            Encoding.UTF8, "application/json"));
    }
}
