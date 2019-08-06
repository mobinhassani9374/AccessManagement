using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Attributes;
using AccessManagement.UI.DataLayer;
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

        public IActionResult Roles(int id)
        {
            return View();
        }
    }
}