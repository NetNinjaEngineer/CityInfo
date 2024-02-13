﻿using CityInfo.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Register")]
    public async Task<ActionResult<AuthModel>> RegisterAsync(RegisterModel registerModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(registerModel);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Login")]
    public async Task<ActionResult<AuthModel>> GetTokenAsync(TokenRequestModel tokenRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(tokenRequest);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _authService.LogoutAsync();
        return Ok("Logged out successfully");
    }

}