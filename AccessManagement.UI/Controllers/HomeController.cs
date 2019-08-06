using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AccessManagement.UI.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var strUserId = HttpContext.User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;

            var userId = Convert.ToInt32(strUserId);

            var userAccess = _context
              .UserAccesses
              .Where(c => c.UserId.Equals(userId))
              .ToList();

            var modules = new SubSystemService()
                .GetAllWithPermision(Assembly.GetExecutingAssembly(), userAccess);

            return View(modules);
        }
    }
}