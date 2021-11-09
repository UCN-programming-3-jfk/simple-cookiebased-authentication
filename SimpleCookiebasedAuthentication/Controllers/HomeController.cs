using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCookiebasedAuthentication.Models;
using System.Diagnostics;

namespace SimpleCookiebasedAuthentication.Controllers
{

    //Cookie based authentication on MSDN:
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

    //Good article about cookie based authentication
    //https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-mvc-using-cookie/


    [Authorize]
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Private()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminsOnly()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}