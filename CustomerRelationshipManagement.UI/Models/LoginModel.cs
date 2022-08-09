using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManagement.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Required")]
        [Display(Name ="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
