using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SendMail
{
    public interface ISendMailServices
    {
        bool SendEmailAsync(string toMail, string voucherCode, string fullName, string Gift);
        bool SendEmailNotifyAsync(string categoryName, int quantity);
    }
}
