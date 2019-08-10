using AccessManagement.UI.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AccessManagement.Attributes
{
    public class HasActionAttribute : Attribute, IFilterFactory
    {
        public string Title { get; set; }

        public string DependTo { get; set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new HasActionInternal(serviceProvider.GetService(typeof(AppDbContext)) as AppDbContext);
        }
    }


    internal class HasActionInternal : ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public HasActionInternal(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            var strUserId = context.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var userId = Convert.ToInt32(strUserId);

            if (_context.UserAccesses.Any(c => c.UserId == userId && c.ControllerName.Equals(controller) && c.ActionName.Equals(action)))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ForbidResult();
            }
            
        }
    }
}
