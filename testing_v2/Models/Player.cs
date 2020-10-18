using System;
using System.Collections.Generic;
using System.Text;

namespace testing_v2.Models
{
	public class Player
	{
		public int NumCoins { get; set; }
		public int Experience { get; set; }
		public int NextCase { get; set; }
		public string ExpLevel { get; set; }
		public BlockStats currblock { get; set; }
		public int total_correct { get; set; }

		public BlockStats caseComplete(bool correct, char casetype)
		{
			if (correct) total_correct++;
			currblock.caseComplete(correct, casetype);
			//if that block is complete return it so it can be added to the completed 
			//BLOCKSIZE DETERIMINED BY THIS MODULO
			if((currblock.CHF + currblock.COPD + currblock.Pneumonia) % 10 == 0)
            {
				BlockStats temp = currblock;
				currblock = new BlockStats(NextCase);
				return currblock;
			}
			NumCoins += 100;
			Experience += 50;
			return null;

		}
		public Player()
		{
			currblock = new BlockStats(0);
			ExpLevel = "undergrad";

		}
	}

}
