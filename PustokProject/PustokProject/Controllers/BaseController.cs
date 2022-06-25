using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PustokProject.Controllers
{
    public class BaseController : Controller
    {
        public string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}

