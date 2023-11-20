using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalTechTools.Models
    {
    public class Product
        {
        public int Id { get; set; }

        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "Product code is required")]
        [Range(0, 99999)]
        public int ProductCode { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Needs to be between 2 and 100 characters long.")]
        public string ProductName { get; set; }

        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product price is required")]
        [Range(0.01, 999.99)]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description is required")]
        [StringLength(maximumLength: 500, MinimumLength = 2, ErrorMessage = "Needs to be between 2 and 500 characters long.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Update Date")]
        public DateTime UpdatedDate { get; set; }
        }
    }
