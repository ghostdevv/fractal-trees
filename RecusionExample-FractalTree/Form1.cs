using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RecusionExample_FractalTree {
    public partial class Form1 : Form
    {
        static Random rnd = new Random();

        private float initialLength = 150;

        private Color backgroundColor = Color.Black;
        private Color baseTreeColor = Color.FromArgb(255, 235, 205);

        private int amountOfTrees = 4;

        public Form1()
        {
            InitializeComponent();

            this.BackColor = this.backgroundColor;
            this.WindowState = FormWindowState.Maximized;

            Color.FromArgb(baseTreeColor.ToArgb());
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            float fraction = (this.Width / this.amountOfTrees) * 0.5F;
            float spacing = (this.Width / this.amountOfTrees) - fraction;

            for (float i = 1; i <= this.amountOfTrees; i++) {
                e.Graphics.TranslateTransform(fraction * i, this.Height);
                Branch(e, this.initialLength, Color.BlanchedAlmond);

                e.Graphics.ResetTransform();
                e.Graphics.TranslateTransform(spacing + i == 1 ? 0 : (fraction * i), 0);
            }
        }

        private void Branch(PaintEventArgs e, float len, Color treeColor)
        {
            float theata;
            GraphicsState graphicsState; 
            Pen pen = new Pen(Color.FromArgb((int)(255 * 0.9 - (len * 0.2)), (int)(235 * 0.9 - (len * 0.6)), (int)(205 * 0.9 - (len * 0.2))), len * 1 / 20);

            e.Graphics.DrawLine(pen, 0, 0, 0, -len);
            pen.Width = pen.Width * 0.66F;

            e.Graphics.TranslateTransform(0, -len);

            len = len * 0.66F;

            if (len > 2.0F)
            {
                graphicsState = e.Graphics.Save();
                theata = rnd.Next(0, 60);

                e.Graphics.RotateTransform(theata);
                Branch(e, len, treeColor);

                e.Graphics.Restore(graphicsState);
                graphicsState = e.Graphics.Save();
                theata = rnd.Next(10, 60);

                e.Graphics.RotateTransform(-theata);
                Branch(e, len, treeColor);

                e.Graphics.Restore(graphicsState);
            }
        }
    }
}
