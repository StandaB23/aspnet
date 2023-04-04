using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceCorp.Data;
using InsuranceCorp.Model;

namespace InsuranceCorp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly InsCorpDbContext _context;

        public PersonController(InsCorpDbContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            return await _context.Persons.Take(100).ToListAsync();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
          if (_context.Persons == null)
          {
              return NotFound();
          }
            var person = await _context.Persons.Include(person => person.Address)
                .Include(person => person.Contracts).FirstOrDefaultAsync(person => person.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // GET: api/Person/a@b.c
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Person>> GetPersonByEmail(string email)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.Include(person => person.Address)
                .Include(person => person.Contracts).FirstOrDefaultAsync(person => person.Email.ToUpper() == email.ToUpper());

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // GET: api/Person/city/Praha
        [HttpGet("city/{mesto}")]
        public async Task<ActionResult<List<Person>>> GetPersonByCity(string mesto)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.Include(person => person.Address).Include(person => person.Contracts)
            .Where(person => person.Address != null && person.Address.City.ToUpper() == mesto.ToUpper()).ToListAsync();

            //return Ok(_context.Persons.Include(person => person.Address).Include(person => person.Contracts)
            //    .Where(person => person.Address != null && person.Address.City.ToUpper() == mesto.ToUpper())
            //    .Select(person => new { person.FirstName, person.LastName, person.Address }).ToList());
                

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.Persons == null)
          {
              return Problem("Entity set 'InsCorpDbContext.Persons'  is null.");
          }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (_context.Persons == null)
            {
                return NotFound();
            }
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return (_context.Persons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
