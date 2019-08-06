using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.UI.DataLayer.Entities
{
    public class UserAccess
    {
        public int Id { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string ActionTitle { get; set; }

        public string ControllerTitle { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
