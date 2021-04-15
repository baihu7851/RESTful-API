using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.ViewModel
{
    public class ViewRole
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}