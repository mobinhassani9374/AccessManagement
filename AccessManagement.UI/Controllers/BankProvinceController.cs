using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.UI.Controllers
{
    [HasModule(Title = "بانک استان ها")]
    public class BankProvinceController : Controller
    {
        [HasAction(Title = "مشاهده استان ها")]
        public IActionResult Index()
        {
            return View();
        }

        [HasAction(Title = "ایجاد استان جدید در سامانه")]
        public IActionResult Create()
        {
            return View();
        }

        [HasAction(Title = "حذف استان در سامانه")]
        public IActionResult Delete()
        {
            return View();
        }

        [HasAction(Title = "ویرایش استان در سامانه")]
        public IActionResult Edit()
        {
            return View();
        }
    }
}