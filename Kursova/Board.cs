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
	class Board
	{
		public static Creature[,] BoardInCreatures;

		public Board(int BoardLength)
		{
			BoardInCreatures = new Creature[BoardLength, BoardLength];
			for (int i = 0; i < BoardLength; i++)
			{
				for (int j = 0; j < BoardLength; j++)
				{
					BoardInCreatures[i, j] = new Death();
				}
			}
		}
		public Board(int BoardLength, int RandomSpawnRange) : this(BoardLength)
		{
			Random r = new Random();
			for (int i = 0; i <= BoardLength * BoardLength / RandomSpawnRange; i++)
			{
				int RandomBirthX = r.Next(0, BoardLength);
				int RandomBirthY = r.Next(0, BoardLength);
				if (r.Next(1, 3) == 1)
				{
					BoardInCreatures[RandomBirthX, RandomBirthY] = new Yang();
				}
				else
				{
					BoardInCreatures[RandomBirthX, RandomBirthY] = new Yin();
				}
			}
		}
		public void Draw()
		{
			Bitmap MyBitmap = new Bitmap(Form1.pictureBox1.Width, Form1.pictureBox1.Height);
			Graphics MyGraphics = Graphics.FromImage(MyBitmap);
			float Delt = ((float)Form1.pictureBox1.Height - 1.5f * 2) / ((float)BoardInCreatures.GetLength(0));
			DrawAllBoards(ref MyGraphics, Delt);
			DrawAllCreatures(ref MyGraphics, Delt);
			Form1.pictureBox1.Image = MyBitmap;
		}
		private void DrawAllBoards(ref Graphics ReceivedGraphics, float Delt)
		{
			for (int i = 0; i <= BoardInCreatures.GetLength(0); i++)
			{
				ReceivedGraphics.DrawLine(new Pen(Color.Black, 3), new PointF(0, 1.5f + (Delt * i)), new PointF(Form1.pictureBox1.Width, 1.5f + (Delt * i)));
			}
			for (int i = 0; i <= BoardInCreatures.GetLength(0); i++)
			{
				ReceivedGraphics.DrawLine(new Pen(Color.Black, 3), new PointF(1.5f + (Delt * i), 0), new PointF(1.5f + (Delt * i), Form1.pictureBox1.Height));
			}
		}
		private void DrawAllCreatures(ref Graphics ReceivedGraphics, float Delt)
		{
			for (int i = 0; i < BoardInCreatures.GetLength(0); i++)
			{
				for (int j = 0; j < BoardInCreatures.GetLength(0); j++)
				{
					if (BoardInCreatures[i, j].Value == 1)
					{
						ReceivedGraphics.FillEllipse(Brushes.White, new RectangleF(Delt * i + 3f, Delt * j + 3f, Delt - 3f, Delt - 3f));
					}
					if (BoardInCreatures[i, j].Value == 2)
					{
						ReceivedGraphics.FillEllipse(Brushes.Black, new RectangleF(Delt * i + 3f, Delt * j + 3f, Delt - 3f, Delt - 3f));
					}
				}
			}
		}
		
		
	}
}
