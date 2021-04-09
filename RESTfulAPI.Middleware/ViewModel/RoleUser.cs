using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Middleware.ViewModel
{
    public class RoleUser
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserDate> UserDates { get; set; }
    }
}