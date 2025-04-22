using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personnel_API.Data;
using static Personnel_API.Model.ApiModel;

namespace Personnel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonnelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetPersonnels()
        {
            return await _context.Personnels.Include(p => p.Sales).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personnel>> GetPersonnel(int id)
        {
            var personnel = await _context.Personnels.Include(p => p.Sales).FirstOrDefaultAsync(p => p.Id == id);

            if (personnel == null)
            {
                return NotFound();
            }

            return personnel;
        }

        [HttpPost]
        public async Task<ActionResult<Personnel>> CreatePersonnel(Personnel personnel)
        {
            if (personnel.Age < 18 || string.IsNullOrEmpty(personnel.Name))
            {
                return BadRequest("Name and Age (above 18) are required.");
            }

            _context.Personnels.Add(personnel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonnel", new { id = personnel.Id }, personnel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonnel(int id, Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return BadRequest();
            }

            _context.Entry(personnel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Personnels.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonnel(int id)
        {
            var personnel = await _context.Personnels.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }

            // Delete associated Sales records
            var salesRecords = _context.Sales.Where(s => s.PersonnelId == id);
            _context.Sales.RemoveRange(salesRecords);

            _context.Personnels.Remove(personnel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
