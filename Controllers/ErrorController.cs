using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExceptionDemo.Models;
using ExceptionDemo.Models.Exceptions;

namespace ExceptionDemo.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch(feature.Error)
            {
                case MyDatabaseException exc:
                    ViewData["Title"] = "Impossibile collegarsi al database";
                    Response.StatusCode = 500;
                    return View();

                default:
                    ViewData["Title"] = "Errore generico";
                    return View();
            }
        }
    }
}
