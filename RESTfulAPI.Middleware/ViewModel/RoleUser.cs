using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Middleware.ViewModel
{
    internal class RoleUser
    {
        public int RolesId { get; set; }
        public List<int> UsersId { get; set; }
    }
}