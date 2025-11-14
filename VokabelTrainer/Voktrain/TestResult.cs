using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class TestResult
    {
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int AskedQuestions { get; set; } = 0;
        public int CorrectAnswers { get; set; } = 0;
        public double PercentageCorrect => AskedQuestions == 0 ? 0 : (double)CorrectAnswers / AskedQuestions * 100;

    }
}
