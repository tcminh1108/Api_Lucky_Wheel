using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.User;
using Services.CategoryVoucher;
using Services.SendMail;
using Services.VipPhones;

namespace VongQuay.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IVoucherService voucherService;
        private readonly ISendMailServices sendMailServices;
        public SendMailController(IUserService userService,
                    IVoucherService voucherService,
                    ISendMailServices sendMailServices)
        {
            this.userService = userService;
            this.voucherService = voucherService;
            this.sendMailServices = sendMailServices;
        }
        [HttpPost("send-mail")]
        public IActionResult SendMail(string phone)
        {
            phone = phone.Trim();
            //kiểm tra số điện thoại hợp lệ chưa
            if (userService.checkPhoneNumber(phone) == false)
            {
                return Ok(new { message = "Vui lòng kiểm tra lại số điện thoại nhập vào" });
            }
            //kiểm tra xem số điện thoại đã có trong DB chưa?
            var checkUser = userService.GetUserByPhoneNumber(phone);
            //số điện thoại chưa tồn tại => chưa quay
            if (checkUser == null)
            {
                return Ok(new { message = "Số điện thoại này chưa tham gia chương trình!" });
            }
            else
            {
                //SendMail
                /*var voucher = voucherService.GetByCode(checkUser.VoucherCodeReceived);*/
                bool isEmailSent = sendMailServices.SendEmailAsync(checkUser.Email, checkUser.VoucherCodeReceived, checkUser.FullName, checkUser.GiftReceived);
                if (isEmailSent)
                {
                    return Ok(new { message = "Email đã được gửi!" });
                }
                else
                {
                    return Ok(new { message = "Email chưa được gửi đi, Vui lòng thử lại." });
                }
            }
        }
    }
}
