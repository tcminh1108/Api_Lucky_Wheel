using Domain.Models;
using Repository.CategoryVoucher;
//using Repository.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoryVoucher
{
    public class VoucherService : IVoucherService
    {
        public readonly IVoucherRepository voucherRepository;
        public VoucherService(IVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
        }
        private VoucherAndQuantity GetVoucher(int categoryId)
        {
            var result = new VoucherAndQuantity();
            var cateVoucher = voucherRepository.SelectCateById(categoryId);
            if (cateVoucher != null)
            {
                if (cateVoucher.Quantity <= 0)
                {
                    var nextCateVoucher = GetnextCateVoucher();
                    if (nextCateVoucher != null)
                    {
                        if (nextCateVoucher.Quantity > 0)
                        {
                            nextCateVoucher.Quantity--;
                        }
                        voucherRepository.UpdateCategory(nextCateVoucher);
                        var voucher = voucherRepository.GetRandomVoucher(nextCateVoucher.Id);
                        voucher.IsUse = true;
                        voucherRepository.UpdateVoucher(voucher);

                        result.Voucher = voucher;
                        result.CategoryName = nextCateVoucher.CategoryName;
                        result.Quantity = nextCateVoucher.Quantity;
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (cateVoucher.Quantity > 0)
                    {
                        cateVoucher.Quantity--;
                    }
                    voucherRepository.UpdateCategory(cateVoucher);
                    var voucher = voucherRepository.GetRandomVoucher(categoryId);
                    voucher.IsUse = true;
                    voucherRepository.UpdateVoucher(voucher);

                    result.Voucher = voucher;
                    result.CategoryName = cateVoucher.CategoryName;
                    result.Quantity = cateVoucher.Quantity;
                    return result;
                }
            }
            return null;
        }

        public Domain.Models.CategoryVoucher GetnextCateVoucher()
        {
            var voucher = voucherRepository.SelectCateById(1);
            if (voucher.Quantity <= 0)
            {
                voucher = voucherRepository.SelectCateById(2);
                if (voucher.Quantity <= 0)
                {
                    voucher = voucherRepository.SelectCateById(3);
                    if (voucher.Quantity <= 0)
                    {
                        voucher = voucherRepository.SelectCateById(4);
                        if (voucher.Quantity <= 0)
                        {
                            return null;
                        }
                    }
                }
            }
            return voucher;
        }

        public VoucherAndQuantity SpinWheel()
        {
            var random = new Random();
            var spinResult = random.Next(1, 101);

            if (spinResult <= 60)
            {
                return GetVoucher(1);
            }
            else if (spinResult <= 80)
            {
                return GetVoucher(2);
            }
            else if (spinResult <= 90)
            {
                return GetVoucher(3);
            }
            else
            {
                return GetVoucher(4);
            }
        }

        public List<Voucher> GetAll()
        {
            List<Domain.Models.Voucher> listVouchers = new List<Domain.Models.Voucher>();
            try
            {
                listVouchers = voucherRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm Voucher bị lỗi");

            }

            return listVouchers;

        }

        public Voucher GetByCode(string VoucherCode)
        {
            return voucherRepository.SelectVoucherByCode(VoucherCode);
        }

        public string GenerateRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
        public void InsertListVoucher(int quantity, int CategoryId)
        {
            var Gift = "";
            if (CategoryId == 1)
            {
                Gift = "200.000 VNĐ";
            }
            if (CategoryId == 2)
            {
                Gift = "300.000 VNĐ";
            }
            if (CategoryId == 3)
            {
                Gift = "Voucher Coffee";
            }
            if (CategoryId == 4)
            {
                Gift = "500.000 VNĐ";
            }
            for (int i = 0; i < quantity; i++)
            {
                var randomCode = GenerateRandomString();
                Domain.Models.Voucher voucher = new Domain.Models.Voucher();
                voucher.VoucherCode = randomCode;
                voucher.Gift = Gift;
                voucher.CategoryId = CategoryId;
                voucher.IsUse = false;
                voucherRepository.InsertVoucher(voucher);
            }
            //cập nhật lại số lượng
            var cateVoucher = voucherRepository.SelectCateById(CategoryId);
            cateVoucher.Quantity += quantity;
            voucherRepository.UpdateCategory(cateVoucher);
        }
    }
}
