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
	public partial class Form1 : Form
	{
		int BoardLength;
		Board BoardMatrix;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			button1.Visible = false;
			button2.Visible = false;
			button3.Visible = false;
			label1.Visible = false;
			label2.Visible = false;
			textBox1.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e) //Start Button
		{
			try
			{
				BoardLength = Convert.ToInt32(textBox1.Text);
				BoardMatrix = new Board(BoardLength, 4);
				timer1.Start();
				label2.Visible = true;
			}
			catch (Exception)
			{
				MessageBox.Show("Write Number in TextField!");
				textBox1.Text = "";
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			GameManager.LoadNextPhase(ref BoardMatrix);
			label2.Text = "Score: " + GameManager.Score.ToString();
			GameManager.VerifyGameEnd();
			label3.Text = "Yang:" + Environment.NewLine + GameManager.YangCount;
			label4.Text = "Yin:" + Environment.NewLine + GameManager.YinCount;
		}

		private void button2_Click(object sender, EventArgs e) //Draw Yang Button
		{
			GameManager.ChooseYinOrYang = new Yang();
		}

		private void button3_Click(object sender, EventArgs e) //Draw Yin Button
		{
			GameManager.ChooseYinOrYang = new Yin();
		}

		private void button4_Click(object sender, EventArgs e) //Play Button
		{
			button4.Visible = false;
			button1.Visible = true;
			button2.Visible = true;
			button3.Visible = true;
			label1.Visible = true;
			textBox1.Visible = true;
		}
	}
}
