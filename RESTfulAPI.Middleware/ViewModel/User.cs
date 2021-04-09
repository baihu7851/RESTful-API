using System;

namespace RESTfulAPI.Middleware.ViewModel
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        private DateTime? Birthday { get; set; }

        public string birthday
        {
            get => Birthday?.ToString("yyyy-MM-dd");
            set => Birthday = Convert.ToDateTime(value);
        }

        public string Email { get; set; }
        public string Phone { get; set; }
        public Link Links { get; set; }
    }
}