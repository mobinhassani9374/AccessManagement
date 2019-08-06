using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagement.Attributes
{
    public class HasActionAttribute : ActionFilterAttribute
    {
        public HasActionAttribute()
        {

        }

        public string Title { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();

            base.OnActionExecuted(context);
        }
    }
}
