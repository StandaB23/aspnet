using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCorp.MVC.Controllers
{
    public class PersonController : Controller
    {
        private InsCorpDbContext _context;

        public PersonController(InsCorpDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // 1. ziskat data
            var top100 = _context.Persons.Include(person => person.Contracts).OrderBy(person => person.Id).Take(100).ToList();

            // 2. zobrazit view
            return View(top100);
        }
        public IActionResult Detail(int id)
        {
            var person = _context.Persons.Find(id);
            if (person == null)
            {
                //return NotFound();
                ViewData["id"] = id;
                return View("NotFound");
            }

            return View(person);
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add(Person person)
        {
            _context.Persons.Add(person);
            var changed = _context.SaveChanges();
            //changed = 0;
            if (changed > 0)
            {
                return Redirect($"/person/detail/{person.Id}");
            } else
            {
                return RedirectToAction("Add");
            }

        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var person = _context.Persons.Find(id);

            return View(person);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Person form_person)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Chyba");
            }

            var db_person = _context.Persons.Find(form_person.Id);

            // 1. moznost zapisu
            db_person.FirstName = form_person.FirstName;
            db_person.LastName = form_person.LastName;
            db_person.Email = form_person.Email;
            db_person.DateOfBirth = form_person.DateOfBirth;
            
            // 2. moznost zapisu (pouzivat pokud mi z formulare prijdou vsechny properties)
            //_context.Entry(db_person).CurrentValues.SetValues(form_person);

            // 3. moznost zapisu (nacteni db_person neni treba) (tuto moznost nepouzivat)
            //_context.Entry(form_person).State = EntityState.Modified;
            
            
            _context.SaveChanges();

            ViewData["succes_message"] = "Uloženo do DB";
            return View(db_person);
        }
        public IActionResult GetByEmail(string email)
        {
            var person = _context.Persons.Where(person => person.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }

            return View("Detail", person);
        }
    }
}
