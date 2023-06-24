using BackendSpotify.Core.Application.Identity.Interfaces;
using BackendSpotify.Host.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BackendSpotify.Host.Identity.Controllers;

public class LoginController : BaseController
{
    private readonly ISpotifyService _spotifyService;
    public LoginController(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    [HttpGet()]
    public async Task<IActionResult> Login()
    {
        var authorizationUrl = await _spotifyService.GetAuthorizationUrl();
        return Redirect(authorizationUrl);
    }


    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code, string state)
    {
        return Ok(await _spotifyService.GetAccessToken(code, state));
    }
}
