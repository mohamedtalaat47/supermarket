using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace supermarket.Dto.UserDTOs
{
    public class UserRegistrationDto : UserDto
    {
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}