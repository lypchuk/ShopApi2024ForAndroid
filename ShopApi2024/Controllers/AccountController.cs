using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ShopApi2024.Constants;
using ShopApi2024.DTOs.Account;
using ShopApi2024.Entities.Identity;
using ShopApi2024.Interfaces;
using ShopApi2024.Services;
using System.Net;
using LoginRequest = ShopApi2024.DTOs.Account.LoginRequest;

namespace ShopApi2024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IImageWorker imageWorker, IMapper mapper, UserManager<UserEntity> userManager) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Registration([FromForm] AccountRegistrationDTO model)
        {
            string image = string.Empty;
            if (model.Image != null)
            {
                image = imageWorker.Save(model.Image).Result;
            }
            else
            {
                image = imageWorker.Save("https://picsum.photos/1200/800?person").Result;
            }

            var user = mapper.Map<UserEntity>(model);
            user.Image = image;

            //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            var result = userManager.CreateAsync(user, model.Password).Result;

            if (!result.Succeeded)
            {
                Console.WriteLine($"--Problem create user--{user.Email}");
            }
            else
            {
                if (model.IsAdmin)
                {
                    result = userManager.AddToRoleAsync(user, Roles.ADMIN).Result;
                }
                else
                {
                    result = userManager.AddToRoleAsync(user, Roles.USER).Result;
                }
            }

            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest loginRequest)
        {
            //=> Ok(await _accountService.LoginAsync(request));
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginRequest.Password))
                throw new HttpException("Invalid login or password", HttpStatusCode.BadRequest);
            return Ok();
        }
        
    }
}
