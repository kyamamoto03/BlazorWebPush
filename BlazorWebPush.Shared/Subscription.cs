namespace BlazorWebPush.Shared;

public record Subscription(
    string EndPoint,
    string P256dh,
    string Auth);