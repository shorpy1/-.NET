using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private Button button1;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new Point(20, 20);
            this.button1.Size = new Size(120, 40);
            this.button1.Text = "Нова зірка";
            this.button1.Click += new EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.button1);
            this.Text = "Кількість зірок: 0";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Номер зірки до створення нової (0, 1, 2, ...)
            int n = Star.Count;

            // Розміри залежать від номера зірки
            int a = 40 + 5 * n;  // зовнішній радіус
            int b = 15 + 2 * n;  // внутрішній радіус

            // Координати теж залежать від номера
            int x = 100 + 60 * n;
            int y = 100 + 30 * n;

            Star myStar = new Star(a, b, x, y, Color.DarkBlue);

            using (Graphics g = this.CreateGraphics())
            {
                myStar.Draw(g);
            }

            this.Text = $"Кількість зірок: {Star.Count}";
        }
    }
}

