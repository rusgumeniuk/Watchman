using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Web.Models;

namespace Watchman.Web.Controllers
{    
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {           
            return View();
        }

        [Authorize]
        public IActionResult Patients()
        {
            IEnumerable<Patient> patients = new List<Patient>();

            return View(patients);
        }
        
    }
}