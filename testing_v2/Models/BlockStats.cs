using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Models
{
    public class BlockStats
    {
        public int startingIndex { get; set; }
        public int COPD { get; set; }
        public int COPDCorrect { get; set; }
        public int CHF { get; set; }
        public int CHFCorrect { get; set; }
        public int Pneumonia { get; set; }
        public int PneumoniaCorrect { get; set; }

        //book keeps the case you passes in
        public void caseComplete(bool correct, char casetype)
        {
            if (casetype == 'c')
            {
                COPD++;
                if (correct) COPDCorrect++;
            }
            if (casetype == 'h')
            {
                CHF++;
                if (correct) CHFCorrect++;
            }
            if (casetype == 'p')
            {
                Pneumonia++;
                if (correct) PneumoniaCorrect++;
            }
        }

        public BlockStats(int startingIndexin)
        {
            startingIndex = startingIndexin;
        }
        public BlockStats()
        {
        }
    }
}
