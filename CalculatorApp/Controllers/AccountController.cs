﻿using CalculatorApp.DTO;
using CalculatorApp.Entities;
using CalculatorApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IRegistrationService _registrationService;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IRegistrationService registrationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _registrationService = registrationService;
        //_tokenService = tokenService;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        //var token = _tokenService.CreateToken(user);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _userManager.ConfirmEmailAsync(user, token);
        await _signInManager.SignInAsync(user, isPersistent: false);


        if (result.Succeeded)
        {
            return new UserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            };
        }


        return Unauthorized();
    }


    [AllowAnonymous]
    [HttpPost("registration/user")]
    public async Task<ActionResult<UserDto>> RegistrationUser(RegisterDto registerDto)
    {
        /*if (await _userManager.Users.AnyAsync(user => user.UserName == registerDto.UserName))
        {
            return BadRequest("UserName is taken");
        }*/
        if (await _userManager.Users.AnyAsync(user => user.Email == registerDto.Email))
        {
            return BadRequest("Email is taken");
        }

        var result = await _registrationService.UserRegistration(registerDto);
        if (result != null)
        {
            return result;
        }

        return BadRequest("Error signup");
    }
    
}