using Microsoft.EntityFrameworkCore;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<QuizItem> QuizItems { get; set; }

        public DbSet<Quiz.Models.ScoreItem> ScoreItems { get; set; }

    }
}
