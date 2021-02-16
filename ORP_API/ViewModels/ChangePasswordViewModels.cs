using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class ChangePasswordViewModels
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword"), Required]
        public string ConfirmPassword { get; set; }
    }
}
