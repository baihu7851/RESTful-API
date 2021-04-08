using System;
using System.Collections.Generic;
using RiskFirst.Hateoas.Models;

namespace RESTfulAPI.Model.ViewModel
{
    public class User : LinkContainer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}