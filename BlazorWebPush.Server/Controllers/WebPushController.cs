using BlazorWebPush.Shared;
using Microsoft.AspNetCore.Mvc;
using PushService.Server;

namespace BlazorTest.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebPushController : ControllerBase
{
    private ILogger<WebPushController> _logger { get; init; }
    private static List<Subscription> _subscriptions { get; } = new();
    private IPushNotification _pushNotification { get; init; }

    public WebPushController(ILogger<WebPushController> logger, IPushNotification pushNotification)
    {
        _logger = logger;
        _pushNotification = pushNotification;

    }

    [HttpPost("Subscribe")]
    public IActionResult Subscribe([FromBody] Subscription subscription)
    {
        try
        {
            _logger.LogInformation("Subscribe: {0}", subscription);
            _subscriptions.Add(subscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Subscribe: {0}", subscription);
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("Send")]
    public async Task<IActionResult> SendAsync()
    {
        try
        {
            _logger.LogInformation("SendAsync");
            foreach (var token in _subscriptions)
            {
                _logger.LogInformation("SendAsync: {0}", token);
                await _pushNotification.PushAsync(token.EndPoint, token.P256dh, token.Auth, "Hello, World!!!!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SendAsync");
            return BadRequest();
        }
        return Ok();
    }
}
