using AccessManagement.UI.DataLayer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AccessManagement.Attributes
{
    public class HasActionAttribute : Attribute
    {
        public string Title { get; set; }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    ////var strUserId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //    ////var userId = Convert.ToInt32(strUserId);

        //    //context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();

        //    base.OnActionExecuted(context);
        //}
    }
}
