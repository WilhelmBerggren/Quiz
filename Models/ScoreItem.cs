using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class ScoreItem
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int Score { get; set; }
    }
}
