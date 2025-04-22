using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personnel_API.Data;
using static Personnel_API.Model.ApiModel;

namespace Personnel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Sales>> CreateSales(Sales sales)
        {
            var personnel = await _context.Personnels.FindAsync(sales.PersonnelId);
            if (personnel == null)
            {
                return NotFound("Personnel not found.");
            }

            _context.Sales.Add(sales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSales", new { id = sales.Id }, sales);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSales(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}
