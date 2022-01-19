using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string userName { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string password { get; set; }
    }
}
