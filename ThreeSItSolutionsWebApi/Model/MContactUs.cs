using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeSItSolutionsWebApi.Models
{
    [Table("MContactUs")]
    public class MContactUs
    {
        [Key]
        public int IID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Your Name")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [Display(Name ="Name")]
        public string CName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Your Email-ID")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [EmailAddress]
        [Display(Name = "Email-Id")]
        public string CEmailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Subject")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [Display(Name = "Subject")]
        public string CSubject { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Your Messages")]
        [StringLength(8000, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [Display(Name = "Message")]
        public string CMessage { get; set; }
        public DateTime? DCreateDate { get; set; } = DateTime.Now;
  
    }
}
