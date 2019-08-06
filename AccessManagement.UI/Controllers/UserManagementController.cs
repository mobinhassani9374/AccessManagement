using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AccessManagement.Attributes;
using AccessManagement.UI.DataLayer;
using AccessManagement.UI.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.UI.Controllers
{
    [HasModule(Title = "مدیریت کاربران")]
    public class UserManagementController : Controller
    {
        private readonly AppDbContext _context;

        public UserManagementController(AppDbContext context)
        {
            _context = context;
        }

        [HasAction(Title = "مشاهده کاربران")]
        public IActionResult Index()
        {
            var model = _context.Users.ToList();
            return View(model);
        }

        [HasAction(Title = "ایجاد کاربر جدید در سامانه")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string fullName, string userName, string password)
        {
            _context.Users.Add(new DataLayer.Entities.User
            {
                FullName = fullName,
                Password = password,
                UserName = userName
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HasAction(Title = "حذف کاربر در سامانه")]
        public IActionResult Delete()
        {
            return View();
        }

        [HasAction(Title = "ویرایش کاربر در سامانه")]
        public IActionResult Edit()
        {
            return View();
        }

        [HasAction(Title = "مدیریت نقش های کاربر")]
        public IActionResult Roles(int id)
        {
            var modules = new SubSystemService()
                .GetAll(Assembly.GetExecutingAssembly());

            ViewBag.UserId = id;

            return View(modules);
        }
        [HttpPost]
        public IActionResult SetRole(int userId, string actionName, string controllerName, bool permision, string controllerTitle, string actionTitle)
        {
            if (permision)
            {
                _context.UserAccesses.Add(new UserAccess
                {
                    ActionName = actionName,
                    ControllerName = controllerName,
                    UserId = userId,
                    ActionTitle = actionTitle,
                    ControllerTitle = controllerTitle
                });
            }
            else
            {
                var entity = _context.UserAccesses
                    .FirstOrDefault(c => c.UserId == userId && c.ControllerName == controllerName && c.ActionName == actionName);

                if (entity != null)
                {
                    _context.Remove(entity);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Roles), new { id = userId });
        }
    }
}