using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("Vouchers")]
    public class Voucher
    {
        [Column("Id")]
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("VoucherCode")]
        [Display(Name = "VoucherCode")]
        [Required(ErrorMessage = "VoucherCode is required")]
        public string VoucherCode { get; set; }

        [Column("Gift")]
        [Display(Name = "Gift")]
        [Required(ErrorMessage = "Gift is required")]
        public string Gift { get; set; }
        public int CategoryId { get; set; }
        public bool IsUse { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryVoucher Category { get; set; }
    }
}
