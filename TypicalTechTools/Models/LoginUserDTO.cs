using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json.Serialization;

namespace TypicalTechTools.Models
    {
    public class LoginUserDTO
        {
        [DisplayName("Email")]
        [Required(ErrorMessage = " Email is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 7, ErrorMessage = "Should be valid email between 7 to 30 character long.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        }
    }
