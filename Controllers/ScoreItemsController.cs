using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;

namespace Quiz.Controllers
{
    [Authorize]
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
            return await _context.ScoreItems.ToListAsync();
        }

        // GET: api/ScoreItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreItem>> GetScoreItem(int id)
        {
            var scoreItem = await _context.ScoreItems.FindAsync(id);

            if (scoreItem == null)
            {
                return NotFound();
            }

            return scoreItem;
        }

        // PUT: api/ScoreItems/5
        [HttpPut("{id}")]
        public IActionResult PutScoreItem(int id, ScoreItem scoreItem)
        {
            return Forbid();
        }

        // POST: api/ScoreItems
        [HttpPost]
        public async Task<ActionResult<ScoreItem>> PostScoreItem(ScoreItem scoreItem)
        {
            scoreItem.Date = DateTime.Now;

            _context.ScoreItems.Add(scoreItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoreItem", new { id = scoreItem.Id }, scoreItem);
        }

        // DELETE: api/ScoreItems/5
        [HttpDelete("{id}")]
        public ActionResult<ScoreItem> DeleteScoreItem(int id)
        {
            return Forbid();
        }
    }
}
