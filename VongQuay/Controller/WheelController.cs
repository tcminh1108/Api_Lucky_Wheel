using Domain.Models;
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
    public class WheelController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IVoucherService voucherService;
        private readonly IVipPhonesServices vipPhonesServices;
        private readonly ISendMailServices sendMailServices;
        public WheelController(IUserService userService, 
                    IVoucherService voucherService, 
                    IVipPhonesServices vipPhonesServices,
                    ISendMailServices sendMailServices)
        {
            this.userService = userService;
            this.voucherService = voucherService;
            this.vipPhonesServices = vipPhonesServices;
            this.sendMailServices = sendMailServices;
        }


        [HttpPost("spin-wheel")]
        public IActionResult SpinWheel(string fullname = "", string phone = "", string email = "")
        {
            fullname = fullname.Trim();
            phone = phone.Trim();
            email = email.Trim();
            //kiểm tra họ tên có hợp lệ không
            if (userService.checkFullName(fullname) == false)
            {
                return Ok(new { message = "Vui lòng kiểm tra lại họ tên nhập vào" });
            }
            //kiểm tra số điện thoại hợp lệ chưa
            if (userService.checkPhoneNumber(phone) == false)
            {
                return Ok(new { message = "Vui lòng kiểm tra lại số điện thoại nhập vào" });
            }

            //kiểm tra email hợp lệ chưa
            if (userService.checkEmailFormat(email) == false)
            {
                return Ok(new { message = "Vui lòng kiểm tra lại email nhập vào" });
            }
            else
            {
                if(userService.checkEmail(email) == false)
                {
                    return Ok(new { message = "Email đã được sử dụng" });
                }
            }

            //kiểm tra xem số điện thoại đã có trong DB chưa?
            var checkUser = userService.GetUserByPhoneNumber(phone);
            //số điện thoại chưa tồn tại => chưa quay
            if (checkUser == null)
            {
                //kiểm tra có nằm trong danh sách VIP không
                var vip = userService.CheckVipPhone(phone);
                //không phải số vip => quay random
                if (vip == null)
                {
                    var voucher = voucherService.SpinWheel();
                    if (voucher != null)
                    {
                        //lưu số điện thoại vừa nhận code
                        var user = new User();
                        user.FullName = fullname;
                        user.Phone = phone;
                        user.Email = email;
                        user.VoucherCodeReceived = voucher.Voucher.VoucherCode;
                        user.GiftReceived = voucher.Voucher.Gift;
                        user.SpinnedDate = DateTime.Now;
                        userService.Insert(user);
                        if(voucher.Quantity < 10)
                        {
                            //Nếu số lượng còn lại của loại voucher đó < 10 thì gửi mail thông báo cho admin
                            sendMailServices.SendEmailNotifyAsync(voucher.CategoryName, voucher.Quantity);
                        }
                        return Ok(voucher.Voucher.Gift);
                    }
                    else
                    {
                        return Ok(new { Message = "Đã hết voucher!" });
                    }
                }
                //số vip => nhả code VIP và lưu vào DB
                else
                {
                    //lưu số điện thoại vừa nhận code
                    var user = new User();
                    user.FullName = fullname;
                    user.Phone = phone;
                    user.Email = email;
                    user.VoucherCodeReceived = vip.VIPCode;
                    user.GiftReceived = vip.Gift;
                    user.SpinnedDate = DateTime.Now;
                    userService.Insert(user);
                    return Ok(vip.Gift);
                }
            }
            //số điện thoại đã tồn tại => thông báo số điện thoại đã tham gia
            else
            {
                return Ok(new { message = "Số điện thoại này đã tham gia" });
            }
        }

        [HttpGet("twenty-closest-spinned")]
        public IActionResult getTwentyUser()
        {
            var listUser = userService.GetAll();
            List<UserSpinnedResult> listResult = new List<UserSpinnedResult>();
            foreach (var user in listUser)
            {
                var hidePhone = user.Phone.Substring(0, user.Phone.Length - 3) + "***";
                try
                {
                    listResult.Add(new UserSpinnedResult
                    {
                        FullName = user.FullName,
                        Gift = user.GiftReceived,
                        SpinnedDate = user.SpinnedDate,
                        Phone = hidePhone
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception("Hệ thống đang bị lỗi, vui lòng thử lại sau!");

                }
                //Danh sách 20 người
                if (listResult.Count == 20)
                {
                    break;
                }
            }
            return Ok(listResult);
        }
    }
}
