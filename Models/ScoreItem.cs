using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class ScoreItem
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public ScoreItem()
        {
            Date = DateTime.Now;
        }
    }
}
