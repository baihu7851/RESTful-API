using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Middleware.ViewModel
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public Link Links { get; set; }
    }
}