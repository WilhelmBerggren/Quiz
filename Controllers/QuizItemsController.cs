using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizItemsController : ControllerBase
    {
        private readonly QuizContext _context;
        private UserManager<IdentityUser> _manager;

        public QuizItemsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/QuizItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizItem>>> GetQuizItems()
        {
            return await _context.QuizItems.ToListAsync();
        }

        // GET: api/QuizItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizItem>> GetQuizItem(int id)
        {
            var quizItem = await _context.QuizItems.FindAsync(id);

            if (quizItem == null)
            {
                return NotFound();
            }

            return quizItem;
        }

        // PUT: api/QuizItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizItem(int id, QuizItem quizItem)
        {
            if (id != quizItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(quizItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizItemExists(id))
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

        // POST: api/QuizItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<QuizItem>> PostQuizItem(QuizItem quizItem)
        {
            _context.QuizItems.Add(quizItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuizItem", new { id = quizItem.Id }, quizItem);
        }

        // DELETE: api/QuizItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizItem>> DeleteQuizItem(int id)
        {
            var quizItem = await _context.QuizItems.FindAsync(id);
            if (quizItem == null)
            {
                return NotFound();
            }

            _context.QuizItems.Remove(quizItem);
            await _context.SaveChangesAsync();

            return quizItem;
        }

        private bool QuizItemExists(int id)
        {
            return _context.QuizItems.Any(e => e.Id == id);
        }
    }
}
