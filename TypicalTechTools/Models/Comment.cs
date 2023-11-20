using System.ComponentModel.DataAnnotations;

namespace TypicalTechTools.Models
    {
    public class Comment
        {
        public int Id { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Needs to be between 2 and 100 characters long.")]
        public string CommentText { get; set; }

        [Display(Name = "Product Code")]
        [Range(0, 99999)]
        public int ProductCode { get; set; }

        public int UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm tt}")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Return a CSV formatted string of the a comment object
        /// </summary>
        /// <returns></returns>
        public string ToCSVString()
            {
            return $"{Id},{CommentText},{ProductCode}";
            }

        }
    }
