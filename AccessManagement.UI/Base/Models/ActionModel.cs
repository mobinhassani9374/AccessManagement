using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagement.Models
{
    public class ActionModel
    {
        public string Title { get; set; }

        public string TitleEn { get; set; }

        public bool HasPermision { get; set; }

        public string DependTo { get; set; }

        public string DependToTitle { get; set; }
    }
}
