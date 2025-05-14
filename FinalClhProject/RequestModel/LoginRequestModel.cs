using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalClhProject.RequestModel
{
    public class LoginRequestModel
    {

        [Required(ErrorMessage = "Enter your Email!!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid E-Mail!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password!!")]
        [MinLength(4, ErrorMessage = "Password must be a minimum of 4 Characters")]
        [MaxLength(15, ErrorMessage = "Password must not be more than 15 Characters")]
        public string Password { get; set; }
    }
}
