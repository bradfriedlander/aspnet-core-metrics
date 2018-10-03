using System.Diagnostics;
using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        public IActionResult Index() => View();
    }
}
