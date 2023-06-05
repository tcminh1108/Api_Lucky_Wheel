using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VipPhones
{
    public interface IVipPhonesRepository
    {
        List<Domain.Models.VipPhone> GetAll();
    }
}
