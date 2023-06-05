using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class WheelController : Controller
    {
        [HttpPost]
        public IActionResult Spin()
        {

            return View();
        }
    }
}
