using System.Security.Cryptography;
using System.Text;
using Api.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using DatingApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController:BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context,ITokenService tokenService)
    {
        _context=context;
        _tokenService=tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) 
        return BadRequest("User Name is already taken");
        using var hmac=new HMACSHA512();
        var user =new AppUser
        {
            UserName=registerDto.Username,
            PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt=hmac.Key
        };
         _context.Users.Add(user);
         await _context.SaveChangesAsync();
        return new UserDto
        {
            Username=user.UserName,
            Token=_tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]

    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user =await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username);
        if(user==null) return Unauthorized("Invalid User Name");
        using var hmac=new HMACSHA512(user.PasswordSalt);
        var commputedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for(int i=0;i<commputedHash.Length;i++)
        {
            if(commputedHash[i]!=user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }
        return new UserDto
        {
            Username=user.UserName,
            Token=_tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string userName)
    {
        return await _context.Users.AnyAsync(x=>x.UserName==userName.ToLower());
    }
}
