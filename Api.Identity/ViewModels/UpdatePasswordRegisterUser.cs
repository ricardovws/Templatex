using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.ViewModels
{
    public class UpdatePasswordRegisterUser
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
