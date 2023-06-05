using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryVoucher
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();
        Domain.Models.CategoryVoucher SelectCateById(int Id);
        Domain.Models.Voucher SelectVoucherByCode(string VoucherCode);
        void UpdateCategory(Domain.Models.CategoryVoucher cv);
        Domain.Models.Voucher GetRandomVoucher(int CategoryId);
        void UpdateVoucher(Domain.Models.Voucher v);
        void InsertVoucher(Domain.Models.Voucher v);

    }
}
