using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext db = null;
        public UserRepository(AppDBContext db)
        {
            this.db = db;
        }

        public Domain.Models.User checkEmail(string email)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any User with email: " + email);
            }
        }

        public Domain.Models.VipPhone CheckVipPhone(string phone)
        {
            try
            {
                return db.VipPhones.FirstOrDefault(vp => vp.Phone == phone);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any User with phone: " + phone);
            }
        }

        public List<Domain.Models.User> GetAll()
        {
            try
            {
                return db.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any User!");
            }
        }

        public Domain.Models.User GetUserByPhoneNumber(string phone)
        {
            try
            {
                return db.Users.FirstOrDefault(u => u.Phone == phone);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any User with phone: " + phone);
            }
        }

        public void Insert(Domain.Models.User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting User");
            }
        }
    }
}
