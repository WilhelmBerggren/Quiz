using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;

namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreItemsController : ControllerBase
    {
        private readonly QuizContext _context;

        public ScoreItemsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/ScoreItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreItem>>> GetScoreItem()
        {
            return await _context.ScoreItem.ToListAsync();
        }

        // GET: api/ScoreItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreItem>> GetScoreItem(int id)
        {
            var scoreItem = await _context.ScoreItem.FindAsync(id);

            if (scoreItem == null)
            {
                return NotFound();
            }

            return scoreItem;
        }

        // PUT: api/ScoreItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoreItem(int id, ScoreItem scoreItem)
        {
            if (id != scoreItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(scoreItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreItemExists(id))
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

        // POST: api/ScoreItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ScoreItem>> PostScoreItem(ScoreItem scoreItem)
        {
            _context.ScoreItem.Add(scoreItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoreItem", new { id = scoreItem.Id }, scoreItem);
        }

        // DELETE: api/ScoreItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScoreItem>> DeleteScoreItem(int id)
        {
            var scoreItem = await _context.ScoreItem.FindAsync(id);
            if (scoreItem == null)
            {
                return NotFound();
            }

            _context.ScoreItem.Remove(scoreItem);
            await _context.SaveChangesAsync();

            return scoreItem;
        }

        private bool ScoreItemExists(int id)
        {
            return _context.ScoreItem.Any(e => e.Id == id);
        }
    }
}
