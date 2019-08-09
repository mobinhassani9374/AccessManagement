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

        [HasAction(Title = "حذف کاربر در سامانه", DependTo = nameof(Index))]
        public IActionResult Delete()
        {
            return View();
        }

        [HasAction(Title = "ویرایش کاربر در سامانه", DependTo = nameof(Index))]
        public IActionResult Edit()
        {
            return View();
        }

        [HasAction(Title = "مدیریت نقش های کاربر", DependTo = nameof(Index))]
        public IActionResult Roles(int id)
        {
            var userAccess = _context
               .UserAccesses
               .Where(c => c.UserId.Equals(id))
               .ToList();

            var modules = new SubSystemService()
                .GetAllWithPermision(Assembly.GetExecutingAssembly(), userAccess);

            ViewBag.UserId = id;

            return View(modules);
        }

        [HttpPost]
        public IActionResult SetRole(int userId, string actionName, string controllerName, bool permision, string controllerTitle, string actionTitle, string dependTo, string dependToTitle)
        {
            if (permision)
            {
                if (!string.IsNullOrEmpty(dependTo))
                {
                    if (!_context.UserAccesses.Any(c => c.UserId.Equals(userId) &&
                     c.ControllerName.Equals(controllerName) &&
                     c.ActionName.Equals(dependTo)))
                    {
                        _context.UserAccesses.Add(new UserAccess
                        {
                            ActionName = dependTo,
                            ActionTitle = dependToTitle,
                            ControllerName = controllerName,
                            ControllerTitle = controllerTitle,
                            UserId = userId
                        });
                    }
                }

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
                var actions = new SubSystemService()
                    .GetAllActions_DependToAction(Assembly.GetExecutingAssembly(),
                    controllerName,
                    actionName);

                var userAccess = _context.UserAccesses.Where(c => c.UserId.Equals(userId)
                  && c.ControllerName.Equals(controllerName)
                  && actions.Contains(c.ActionName))
                  .ToList();

                userAccess.ForEach(c=> 
                {
                    _context.Remove(c);
                });

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