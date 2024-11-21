using CWB.App.Models.Routing;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class EmployeeMasterController : Controller
    {

        private readonly ILoggerManager _logger;


        public EmployeeMasterController(ILoggerManager logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }

}
