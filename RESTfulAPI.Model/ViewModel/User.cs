using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Model.ViewModel
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> Links { get; set; }
    }
}