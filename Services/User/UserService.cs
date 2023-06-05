using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repository.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool checkEmail(string email)
        {
            if(userRepository.checkEmail(email) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkEmailFormat(string email)
        {
            if (email == "")
            {
                return false;
            }
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!regex.IsMatch(email))
            {
                return false;
            }
            return true;
        }

        public bool checkPhoneNumber(string phone)
        {
            if (phone == null)
            {
                return false;
            }
            if (phone == "")
            {
                return false;
            }

            // Kiểm tra có chứa ký tự đặc biệt hay khoảng trắng hay không
            if (Regex.IsMatch(phone, @"[^0-9]"))
            {
                return false;
            }

            // Kiểm tra độ dài của số điện thoại
            if (phone.Length != 10)
            {
                return false;
            }

            // Kiểm tra định dạng số điện thoại theo đúng quy tắc của Việt Nam
            var first2Number = phone.Substring(0, 2);

            if (first2Number != "03"
                && first2Number != "05"
                && first2Number != "07"
                && first2Number != "08"
                && first2Number != "09")
            {
                return false;
            }

            // Nếu điều kiện kiểm tra định dạng số điện thoại đều thỏa mãn, trả về true
            return true;

        }
        public bool checkFullName(string fullname)
        {
            if (fullname == "")
            {
                return false;
            }
            if (fullname == null)
            {
                return false;
            }

            // Kiểm tra có chứa ký tự đặc biệt, số hay không
            if (Regex.IsMatch(fullname, @"[^a-zA-Z\u00C0-\u1EF9 ]"))
            {
                return false;
            }

            // Kiểm tra có chứa khoảng trắng liền nhau hay không
            if (Regex.IsMatch(fullname, @"\s{2,}"))
            {
                return false;
            }

            return true;
        }
        public Domain.Models.VipPhone CheckVipPhone(string phone)
        {
            return userRepository.CheckVipPhone(phone);
        }

        public List<Domain.Models.User> GetAll()
        {
            List<Domain.Models.User> listUsers = new List<Domain.Models.User>();
            try { 
                listUsers = userRepository.GetAll();
                listUsers = listUsers.OrderByDescending(x => x.SpinnedDate).ToList();
            } catch(Exception ex)
            {
                throw new Exception("Tìm kiếm user bị lỗi");

            }

            return listUsers;
        }

        public Domain.Models.User GetUserByPhoneNumber(string phone)
        {
            return userRepository.GetUserByPhoneNumber(phone);
        }

        public void Insert(Domain.Models.User user)
        {
            userRepository.Insert(user);
        }
    }
}
