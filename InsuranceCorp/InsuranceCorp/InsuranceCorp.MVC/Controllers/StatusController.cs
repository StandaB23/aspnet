using InsuranceCorp.Data;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.MVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly InsCorpDbContext _context;
        public StatusController(InsCorpDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            bool ok = _context.Database.CanConnect();

            ViewData["ok"] = ok;

            return View();
        }
    }
}
