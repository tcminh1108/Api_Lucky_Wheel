using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SendMail
{
    public class SendMailServices : ISendMailServices
    {
        private readonly IConfiguration _configuration;
        public SendMailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmailAsync(string toMail, string voucherCode, string fullName, string Gift)
        {
            var username = _configuration["MailConfig:username"];
            var password = _configuration["MailConfig:password"];
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(toMail));
            var bccAddress = new MailboxAddress("admin", username);

            email.Bcc.Add(bccAddress);
            email.Subject = "THƯ CHÚC MỪNG QUAY SỐ TRÚNG THƯỞNG";

            var builder = new BodyBuilder();


            builder.HtmlBody = "<div style=\"font-family: Helvetica, Arial, sans-serif\">    \r\n    \r\n   " +
                " <div>Xin chào <span style=\"font-weight: bold;\">"+ fullName + "</span>,</div>  <br>    \r\n    " +
                "<div>Trước tiên, đại diện của <span style=\"font-weight: bold; color: #FF630E;\">BSmart</span>\r\n        " +
                "xin gửi lời cảm ơn chân thành đến bạn vì đã tham gia chương trình \r\n         " +
                "<span style=\"font-weight: bold;\">\"Quay Số Trúng Thưởng\"\r\n       " +
                " </span>  \r\n        của chúng tôi.\r\n        </div> \r\n        " +
                "<br>\r\n     <div>\r\n        Chúc mừng bạn đã trúng giải thưởng trị giá \r\n        <span style=\"font-weight: bold;\">\""+ Gift + "\".  \r\n      " +
                "  </span>\r\n        <br>\r\n       " +
                " Mã số Voucher: <span style=\"font-weight: bold;\">\"" + voucherCode + "\".\r\n     " +
                "   </span>  \r\n      \r\n     </div>    \r\n     " +
                "   <br>\r\n    <div>Nếu bạn có bất kỳ câu hỏi hoặc cần hỗ trợ nào khác, vui lòng liên hệ với chúng tôi qua email hoặc số điện thoại trung tâm mà chúng tôi cung cấp trên trang web của BSmart.</div>\r\n    <br>  \r\n    <div>Một lần nữa, <span style=\"font-weight: bold; color: #FF630E;\">BSmart</span>  muốn bày tỏ lòng biết ơn sâu sắc đến bạn vì đã tham gia chương trình khuyến mãi của chúng tôi.\r\n    </div> \r\n    <br />\r\n     <div>Chúc bạn một trải nghiệm học tập thú vị và thành công trong sự nghiệp của mình. BSmart hy vọng sẽ tiếp tục có cơ hội hợp tác với bạn trong tương lai.</div><br><br>\r\n</div>\r\n"
                + "<div style=\"font-weight: bold;\">Trân trọng, <br>\r\n        <div style=\"color: #FF630E;\">Bộ phận hỗ trợ học viên BSMART</div>\r\n    </div>\r\n<br>    <img src=\"cid:image1\" alt=\"\" width=\"200px\">\r\n    <br>\r\n    <br>\r\n    <div>\r\n        Email liên hệ: admin@bsmart.edu.vn\r\n    </div>\r\n    <div>Số điện thoại: 028 9999 79 39</div>\r\n</div>";
            // Khởi tạo phần đính kèm của email (ảnh)
            var attachment = builder.LinkedResources.Add("Files/Images/icon-logo-along.webp");
            attachment.ContentId = "image1"; // Thiết lập Content-ID cho phần đính kèm

            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            /*smtp.Authenticate("phannhattan201447@gmail.com", "wmqltqvnpuikinxm");*/
            smtp.Authenticate(username, password);
            try
            {
                smtp.Send(email);

            }
            catch (SmtpCommandException ex)
            {
                return false;
            }
            smtp.Disconnect(true);
            return true;
        }

        public bool SendEmailNotifyAsync(string categoryName, int quantity)
        {
            var username = _configuration["MailConfig:username"];
            var password = _configuration["MailConfig:password"];
            var email = new MimeMessage();


            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(username));

            email.Subject = "THÔNG BÁO BỔ SUNG VOUCHER";

            var builder = new BodyBuilder();


            builder.HtmlBody = "<div>\r\n  <h2> Code " + categoryName + " còn lại số lượng " + quantity + "</h1>\r\n  <h3> Vui lòng bổ sung voucher </h3>\r\n</div>";

            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(username, password);
            try
            {
                smtp.Send(email);

            }
            catch (SmtpCommandException ex)
            {
                return false;
            }
            smtp.Disconnect(true);
            return true;
        }
    }
}
