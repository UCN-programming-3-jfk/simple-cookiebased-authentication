using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SimpleCookiebasedAuthentication.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleCookiebasedAuthentication.Controllers
{
    public class AccountsController : Controller
    {

        //shows the login form
        [HttpGet]
        public IActionResult Login() => View(); 

        //receives the login form on submit
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel loginInfo, [FromQuery] string returnUrl) 
        {

            if (loginInfo.UserName == "Bob" && loginInfo.Password == "1234")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Bob"),
                    new Claim(ClaimTypes.Role, "Registered_user"),
                };

                 await SignInUsingClaims(claims);
            }
            else if (loginInfo.UserName == "Admin" && loginInfo.Password == "1234")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                  await SignInUsingClaims(claims);
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction();
            }
            return View();
        }

        //creates the authentication cookie with claims
        private async Task SignInUsingClaims(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                #region often used options - to consider including in cookie
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value. 
                #endregion
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["Message"] = $"You are logged in as {claimsIdentity.Name}";
        }

        //deletes the authentication cooke
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "You are now logged out.";
            return RedirectToAction("Index", "");
        }

        //displayed if an area is off-limits, based on an authenticated user's claims
        public  IActionResult AccessDenied()
        {
            return View();
        }
    }
}