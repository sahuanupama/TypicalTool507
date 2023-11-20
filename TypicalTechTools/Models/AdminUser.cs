using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TypicalTechTools.Models
    {
    public class AdminUser
        {
        public int Id { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 7, ErrorMessage = "Should be valid email between 7 to 30 character long.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Should be between 5 to 30 character long.")]
        public string Password { get; set; }
        }
    }
