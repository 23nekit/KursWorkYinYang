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
	class GameManager
	{
		public static Creature ChooseYinOrYang;
		public static float Score;

		private static int YangCountInNumber;
		private static int YinCountInNumber;
		private static bool IsMessageOfRestartGameShow = false;
		private static int Phase = 1;

		public static string YangCount
		{
			get
			{
				CountCreatures(new Yang(), ref YangCountInNumber);
				return YangCountInNumber.ToString();
			}
		}
		public static void CountCreatures(Creature counterCreature, ref int Counter)
		{
			Counter = 0;
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value == counterCreature.Value)
					{
						Counter += 1;
					}
				}
			}
		}

		public static string YinCount
		{
			get
			{
				CountCreatures(new Yin(), ref YinCountInNumber);
				return YinCountInNumber.ToString();
			}
		}

		public static void VerifyGameEnd()
		{
			bool IsBoardHaveYin = false;
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value == 1)
					{
						IsBoardHaveYin = true;
						break;
					}
				}
			}
			bool IsBoardHaveYang = false;
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value == 2)
					{
						IsBoardHaveYang = true;
						break;
					}
				}
			}

			if (!(IsBoardHaveYang && IsBoardHaveYin))
			{
				DialogResult RestartGame = new DialogResult();
				if (!IsMessageOfRestartGameShow)
				{
					IsMessageOfRestartGameShow = true;
					RestartGame = MessageBox.Show("Game over", "", MessageBoxButtons.OK);
				}
				if (RestartGame == DialogResult.OK)
				{
					Application.Restart();
				}
			}
		}
		public static void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			float x = e.X;
			float y = e.Y;
			if (ChooseYinOrYang != null)
			{
				int NewX = (int)((int)x / ((float)Form1.pictureBox1.Width / Board.BoardInCreatures.GetLength(0)));
				int NewY = (int)((int)y / ((float)Form1.pictureBox1.Height / Board.BoardInCreatures.GetLength(0)));
				Board.BoardInCreatures[NewX, NewY] = ChooseYinOrYang;
				ScoreObject NewScoreObject = new ScoreObject(NewX, NewY, ChooseYinOrYang);
			}
		}

		public static void LoadNextPhase(ref Board ReceivedBoard)
		{
			if (Phase == 1)
			{
				LoadBirth();
				ReceivedBoard.Draw();
				Phase += 1;
			}
			else if (Phase == 2)
			{
				LoadOverpopulationOrSolitude();
				ReceivedBoard.Draw();
				Phase += 1;
			}
			else if (Phase == 3)
			{
				LoadDeathInAnUnequalConfrontation();
				ReceivedBoard.Draw();
				Phase = 1;
			}
		}
		private static void LoadBirth()
		{
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value == 0)
					{
						AddNewCreatureIfNeedIt(i, j);
					}
				}
			}
		}
		private static void AddNewCreatureIfNeedIt(int i, int j)
		{
			int CountOfNeighboors = 0;
			for (int x = i - 1; x < i + 2; x++)
			{
				for (int y = j - 1; y < j + 2; y++)
				{
					if (x >= 0 && x < Board.BoardInCreatures.GetLength(0) && y >= 0 && y < Board.BoardInCreatures.GetLength(0))
					{
						if (Board.BoardInCreatures[x, y].Value != 0)
						{
							CountOfNeighboors += 1;
						}
					}
				}
			}
			if (CountOfNeighboors == 3)
			{
				int Counter = 0;
				for (int x = i - 1; x < i + 2; x++)
				{
					for (int y = j - 1; y < j + 2; y++)
					{
						if (x >= 0 && x < Board.BoardInCreatures.GetLength(0) && y >= 0 && y < Board.BoardInCreatures.GetLength(0))
						{
							Counter += Board.BoardInCreatures[x, y].Value;
						}
					}
				}
				if (Counter == 4)
				{
					Board.BoardInCreatures[i, j] = new Yin();
				}
				if (Counter == 5)
				{
					Board.BoardInCreatures[i, j] = new Yang();
				}
			}
		}
		private static void LoadOverpopulationOrSolitude()
		{
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value != 0)
					{
						KillCreatureIfNeedIt(i, j);
					}
				}
			}
		}
		private static void KillCreatureIfNeedIt(int i, int j)
		{
			int CountOfNeighboors = 0;
			for (int x = i - 1; x < i + 2; x++)
			{
				for (int y = j - 1; y < j + 2; y++)
				{
					if (x >= 0 && x < Board.BoardInCreatures.GetLength(0) && y >= 0 && y < Board.BoardInCreatures.GetLength(0))
					{
						if (Board.BoardInCreatures[x, y].Value != 0)
						{
							if (x != i || y != j)
							{
								CountOfNeighboors += 1;
							}
						}
					}
				}
			}
			if (CountOfNeighboors > 4 || CountOfNeighboors < 2)
			{
				Board.BoardInCreatures[i, j] = new Death();
			}
		}
		private static void LoadDeathInAnUnequalConfrontation()
		{
			for (int i = 0; i < Board.BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < Board.BoardInCreatures.GetLength(0); j++)
				{
					if (Board.BoardInCreatures[i, j].Value != 0)
					{
						KillCreatureIfNeedIt(i, j, 4);
					}
				}
			}
		}
		private static void KillCreatureIfNeedIt(int i, int j, int CountOfNeighboorsForKill)
		{
			int CountOfNeighboors = 0;
			for (int x = i - 1; x < i + 2; x++)
			{
				for (int y = j - 1; y < j + 2; y++)
				{
					if (x >= 0 && x < Board.BoardInCreatures.GetLength(0) && y >= 0 && y < Board.BoardInCreatures.GetLength(0))
					{
						if (Board.BoardInCreatures[x, y].Value != 0)
						{
							if (x != i || y != j)
							{
								CountOfNeighboors += 1;
							}
						}
					}
				}
			}
			if (CountOfNeighboors == CountOfNeighboorsForKill)
			{
				int Counter = 0;
				for (int x = i - 1; x < i + 2; x++)
				{
					for (int y = j - 1; y < j + 2; y++)
					{
						if (x >= 0 && x < Board.BoardInCreatures.GetLength(0) && y >= 0 && y < Board.BoardInCreatures.GetLength(0))
						{
							if (x != i || y != j)
							{
								Counter += Board.BoardInCreatures[x, y].Value;
							}
						}
					}
				}
				if (Counter == 7 && Board.BoardInCreatures[i, j].Value == 1)
				{
					Board.BoardInCreatures[i, j] = new Death();
				}
				if (Counter == 5 && Board.BoardInCreatures[i, j].Value == 2)
				{
					Board.BoardInCreatures[i, j] = new Death();
				}
			}
		}
	}
}
