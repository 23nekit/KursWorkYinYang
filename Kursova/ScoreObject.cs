using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursova
{
	class ScoreObject
	{
		private Timer ScoreTimer;
		private int TimeForAddScore;
		private Point ScoreObjectCoordinatesInBoard;
		private Creature ThisCreature;

		public ScoreObject(int i, int j, Creature creature)
		{
			ScoreObjectCoordinatesInBoard = new Point(i, j);
			ThisCreature = creature;
			ScoreTimer = new Timer();
			ScoreTimer.Tick += new System.EventHandler(ScoreTimerTick);
			TimeForAddScore = 0;
			ScoreTimer.Interval = 1000;
			ScoreTimer.Start();
		}

		internal GameManager CreateWhenAddOnPictureBoxCreature
		{
			get => default(GameManager);
			set
			{
			}
		}

		private void ScoreTimerTick(object sender, EventArgs e)
		{
			if (TimeForAddScore != 5)
			{
				TimeForAddScore += 1;
			}
			else
			{
				if (Board.BoardInCreatures[ScoreObjectCoordinatesInBoard.X, ScoreObjectCoordinatesInBoard.Y] == ThisCreature) 
				{
					GameManager.Score += 100;
					ScoreTimer.Stop();
				}
			}
		}
	}
}
