using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagement.Models
{
    public class ModuleModel
    {
        public string TitleEn { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Image { get; set; }

        public bool HasPermision { get; set; }

        public List<ActionModel> Actions { get; set; } = new List<ActionModel>();
    }
}
