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

        public string DependTo { get; set; }
    }
}
