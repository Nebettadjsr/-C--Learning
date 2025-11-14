using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class Statistik
    {
        public int GefragtGesamt { get; set; } = 0;
        public int GefragtRichtig { get; set; } = 0;
        public double GefragtProzentRichtig => GefragtGesamt == 0 ? 0 : (double)GefragtRichtig / GefragtGesamt * 100;
        public Difficulty DifficultyLevel { get; set; }

        public List<TestResult> TestErgebnisse { get; set; } = new List<TestResult>();

        public void AddTest(TestResult result)
        {
            TestErgebnisse.Add(result);
            GefragtGesamt += result.AskedQuestions;
            GefragtRichtig += result.CorrectAnswers;           
        }
        public (int totalQAsked, int totalQCorrect, double percentCorrect, int testCount) GetStatisticsSummary(Difficulty diff)
        {
            var tests = TestErgebnisse.Where(t => t.Difficulty == diff);  

            int totalQAsked = tests.Sum(t => t.AskedQuestions);
            int totalQCorrect = tests.Sum(t => t.CorrectAnswers);
            int testCount = tests.Count();
            double percentCorrect = totalQAsked == 0 ? 0 : (double)totalQCorrect / totalQAsked * 100;

            /*
            Console.WriteLine($"Statistik für {DifficultyLevel} Vokabeln:");
            Console.WriteLine($"Gesamt gefragt: {totalQAsked}");
            Console.WriteLine($"Richtig beantwortet: {totalQCorrect}");
            Console.WriteLine($"Prozent richtig: {percentCorrect:F2}%");
            Console.WriteLine($"Anzahl Tests: {TestErgebnisse.Count}");
            */

            return (totalQAsked, totalQCorrect, percentCorrect, testCount);
        }
    }
}
