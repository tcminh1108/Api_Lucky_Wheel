using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("FullName")]
        [Display(Name = "FullName")]
        [StringLength(50, ErrorMessage = ("Full name must be less than 50 characters"))]
        public string FullName { get; set; }

        [Column("Phone")]
        [Display(Name = "Phone")]
        [StringLength(11, ErrorMessage = ("Phone must be less than 10 or 11 number"))]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Column("Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Column("VoucherCodeReceived")]
        [Display(Name = "VoucherCodeReceived")]
        [Required(ErrorMessage = "Voucher Code Received is required")]
        public string VoucherCodeReceived { get; set; }

        [Column("GiftReceived")]
        [Display(Name = "GiftReceived")]
        [Required(ErrorMessage = "Gift Received is required")]
        public string GiftReceived { get; set; }

        [Column("SpinnedDate")]
        [Display(Name = "SpinnedDate")]
        public DateTime SpinnedDate { get; set; }

    }
}
