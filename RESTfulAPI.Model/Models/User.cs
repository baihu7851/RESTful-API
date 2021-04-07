using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulAPI.Model.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }

        public DateTime? Birthday { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}