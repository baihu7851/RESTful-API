using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Middleware.ViewModel
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}