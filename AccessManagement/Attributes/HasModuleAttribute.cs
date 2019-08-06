using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagement.Attributes
{
    public class HasModuleAttribute : Attribute
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public string Image { get; set; }
    }
}
