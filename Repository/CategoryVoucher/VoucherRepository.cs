using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryVoucher
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDBContext db = null;
        public VoucherRepository(AppDBContext db)
        {
            this.db = db;
        }

        public List<Voucher> GetAll()
        {
            try
            {
                return db.Vouchers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any Voucher!");
            }
        }

        public Voucher GetRandomVoucher(int CategoryId)
        {
            var voucher = db.Vouchers
            .Where(v => v.CategoryId == CategoryId && v.IsUse == false).FirstOrDefault();
            return voucher;
        }

        public void InsertVoucher(Voucher v)
        {
            try
            {
                db.Vouchers.Add(v);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting Voucher");
            }
        }

        public Domain.Models.CategoryVoucher SelectCateById(int Id)
        {
            return db.CategoryVouchers.Where(cv => cv.Id == Id).FirstOrDefault();
        }

        public Voucher SelectVoucherByCode(string VoucherCode)
        {
            return db.Vouchers.Where(v => v.VoucherCode == VoucherCode).FirstOrDefault();
        }

        public void UpdateCategory(Domain.Models.CategoryVoucher cv)
        {
            try
            {
                db.CategoryVouchers.Update(cv);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any Category Vouchers with Id: " + cv.Id);
            }
        }

        public void UpdateVoucher(Voucher v)
        {
            try
            {
                db.Vouchers.Update(v);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any Category Vouchers with Id: " + v.Id);
            }
        }
    }
}
