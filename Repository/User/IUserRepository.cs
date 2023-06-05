using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.User
{
    public interface IUserRepository
    {
        List<Domain.Models.User> GetAll();
        Domain.Models.User GetUserByPhoneNumber(string phone);
        Domain.Models.VipPhone CheckVipPhone(string phone);
        void Insert(Domain.Models.User user);
        Domain.Models.User checkEmail(string email);
    }
}
