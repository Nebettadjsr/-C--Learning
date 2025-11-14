using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voktrain
{
    public class Vokabel
    {
        public string Deutsch { get; set; }
        public string Englisch { get; set; }
        public int GeuebtGesamt { get; set; } = 0;
        public int RichtigGesamt { get; set; } = 0;
        public double ProzentRichtig => GeuebtGesamt == 0 ? 0 : (double)RichtigGesamt / GeuebtGesamt * 100;

        /* double ProzentRichtig => GeuebtGesamt == 0 ? 0 : (double)RichtigGesamt / GeuebtGesamt * 100;

        double ProzentRichtig
        {
            get
            {
                // if no attempts yet, avoid dividing by zero
                if (GeuebtGesamt == 0)
                {
                    return 0;
                }
                else
                {
                    // calculate correct / total * 100 as double
                    double prozent = (double)RichtigGesamt / GeuebtGesamt * 100;
                    return prozent;
                }
            }
        }
           
        if (?) [GeuebtGesamt == 0, then return 0,]
        else (:) [return (double)RichtigGesamt / GeuebtGesamt * 100]
         
         */

        public Difficulty Schwierigkeit { get; set; } 

        public Vokabel(string deutsch, string englisch)
        {
            Deutsch = deutsch;
            Englisch = englisch;
          
        }
    }
}
