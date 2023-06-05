using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.VipPhones
{
    public interface IVipPhonesServices
    {
        List<Domain.Models.VipPhone> GetAll();

    }
}
