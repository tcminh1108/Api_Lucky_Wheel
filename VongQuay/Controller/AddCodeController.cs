using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.User;
using Services.CategoryVoucher;
using Services.SendMail;

namespace VongQuay.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCodeController : ControllerBase
    {
        private readonly IVoucherService voucherService;
        public AddCodeController(IVoucherService voucherService)
        {
            this.voucherService = voucherService;
        }
        [HttpGet("add-code")]
        public IActionResult AddCode(int quantity, int CategoryId)
        {
            voucherService.InsertListVoucher(quantity, CategoryId);
            return Ok(new { message = "Đã thêm xong" });
        }
    }
}
