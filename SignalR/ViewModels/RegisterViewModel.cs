using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [Remote("IsUserExist", "Account", ErrorMessage = "نام کاربری موجود می باشد")]
        public string userName { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
        public string nameFamily { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string password { get; set; }
        
        [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید")]
        [Compare(nameof(password), ErrorMessage = "رمز عبور با تکرار رمز عبور برابر نیست")]
        public string rePassword { get; set; }
    }
}
