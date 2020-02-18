using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelWebApp.Models
{
    public class Model
    {
        
    }

    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
            [Required(ErrorMessage = "Please enter user name.")]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Please enter password.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string UserPassword { get; set; }

            

    }
}
