using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("VipPhones")]
    public class VipPhone
    {
        [Column("Id")]
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Phone")]
        [Display(Name = "Phone")]
        [StringLength(11, ErrorMessage = ("Phone must be less than 10 or 11 number"))]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Column("VIPCode")]
        [Display(Name = "VIPCode")]
        [Required(ErrorMessage = "VIPCode is required")]
        public string VIPCode { get; set; }

        [Column("Gift")]
        [Display(Name = "Gift")]
        [Required(ErrorMessage = "Gift is required")]
        public string Gift { get; set; }
    }
}
