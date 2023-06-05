using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VipPhones
{
    public class VipPhonesRepository : IVipPhonesRepository
    {
        private readonly AppDBContext db = null;
        public VipPhonesRepository(AppDBContext db)
        {
            this.db = db;
        }

        public List<VipPhone> GetAll()
        {
            try
            {
                return db.VipPhones.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't find any phone");
            }
        }
    }
}
