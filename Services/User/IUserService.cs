using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.User
{
    public interface IUserService
    {
        List<Domain.Models.User> GetAll();
        bool checkFullName(string fullname);
        bool checkPhoneNumber(string phone);
        bool checkEmailFormat(string email);
        bool checkEmail(string email);
        Domain.Models.User GetUserByPhoneNumber(string phone); 
        Domain.Models.VipPhone CheckVipPhone(string phone);
        void Insert(Domain.Models.User user);
    }
}
