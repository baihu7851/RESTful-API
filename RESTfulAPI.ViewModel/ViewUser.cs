using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.ViewModel

{
    public class ViewUser
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}