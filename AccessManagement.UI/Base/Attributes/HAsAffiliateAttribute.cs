using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Attributes
{
    public class HasAffiliateAttribute : Attribute
    {
        public string ActionName { get; set; }
    }
}
