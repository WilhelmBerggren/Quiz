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

namespace Quiz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizItemsController : ControllerBase
    {
        private readonly QuizContext _context;

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
        [HttpPut("{id}")]
        public IActionResult PutQuizItem(int id, QuizItem quizItem)
        {
            return Forbid();
        }

        // POST: api/QuizItems
        [HttpPost]
        public ActionResult<QuizItem> PostQuizItem(QuizItem quizItem)
        {
            return Forbid();
        }

        // DELETE: api/QuizItems/5
        [HttpDelete("{id}")]
        public ActionResult<QuizItem> DeleteQuizItem(int id)
        {
            return Forbid();
        }
    }
}
