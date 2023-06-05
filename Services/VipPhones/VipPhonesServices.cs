using Domain.Models;
using Repository.CategoryVoucher;
using Repository.VipPhones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.VipPhones
{
    public class VipPhonesServices : IVipPhonesServices
    {
        public readonly IVipPhonesRepository vipPhonesRepository;
        public VipPhonesServices(IVipPhonesRepository vipPhonesRepository)
        {
            this.vipPhonesRepository = vipPhonesRepository;
        }
        public List<VipPhone> GetAll()
        {
            List<Domain.Models.VipPhone> listVipPhones = new List<Domain.Models.VipPhone>();
            try
            {
                listVipPhones = vipPhonesRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm phone bị lỗi");

            }

            return listVipPhones;
        }
    }
}
