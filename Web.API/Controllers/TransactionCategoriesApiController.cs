using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransactionCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionCategory>>> GetTransactionCategories()
        {
            return await _context.TransactionCategories.ToListAsync();
        }

        // GET: api/TransactionCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionCategory>> GetTransactionCategory(int id)
        {
            var category = await _context.TransactionCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/TransactionCategories
        [HttpPost]
        public async Task<ActionResult<TransactionCategory>> PostTransactionCategory(TransactionCategory category)
        {
            _context.TransactionCategories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionCategory), new { id = category.Id }, category);
        }

        // PUT: api/TransactionCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionCategory(int id, TransactionCategory category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // DELETE: api/TransactionCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionCategory(int id)
        {
            var category = await _context.TransactionCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.TransactionCategories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.TransactionCategories.Any(e => e.Id == id);
        }
    }
}
