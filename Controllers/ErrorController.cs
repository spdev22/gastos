using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonalExpenses.Controllers
{
    public class ErrorController : Controller
    {
        // This action handles 404 errors
        // It can be called when a requested resource is not found
        // The route is defined in Program.cs to handle status code pages

        [Route("/Error/404")]
        public IActionResult Error404()
        {
            return View();
        }
    }
}