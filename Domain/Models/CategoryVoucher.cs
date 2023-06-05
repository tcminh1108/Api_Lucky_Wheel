using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    [Table("CategoryVouchers")]
    public class CategoryVoucher
    {
        [Column("Id")]
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("CategoryName")]
        [Display(Name = "CategoryName")]
        [StringLength(100, ErrorMessage = ("Category Name must be less than 100 characters"))]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        [Column("Quantity")]
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
