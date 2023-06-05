using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoryVoucher
{
    public interface IVoucherService
    {
        List<Voucher> GetAll();
        Domain.Models.Voucher GetByCode(string VoucherCode);
        VoucherAndQuantity SpinWheel();
        void InsertListVoucher(int quantity, int CategoryId);
    }
}
