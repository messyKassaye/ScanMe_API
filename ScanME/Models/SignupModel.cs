using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class SignupModel
    {
        [Required(ErrorMessage ="Full name is not added")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Phone number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
