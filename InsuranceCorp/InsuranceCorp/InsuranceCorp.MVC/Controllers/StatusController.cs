using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.MVC.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
