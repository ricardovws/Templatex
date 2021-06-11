using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.ViewModels
{
    public class UpdateEmailRegisterUser
    {
        public string NewEmail { get; set; }
        public string Password { get; set; }
    }
}
