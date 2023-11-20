using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TypicalTechTools.Models
    {
    public class CreateUserDTO : LoginUserDTO
        {
        [DisplayName("Password Confirmation")]
        [Required(ErrorMessage = " Password Confirmation is required.")]
        public string PasswordConfirmation { get; set; }
        }
    }
