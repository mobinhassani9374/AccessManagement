using AccessManagement.UI.DataLayer;
using Microsoft.AspNetCore.Authentication;
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
            serviceProvider.GetService(typeof(AppDbContext));
        }
    }
}
