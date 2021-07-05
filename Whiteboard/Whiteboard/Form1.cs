using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Whiteboard.MoveShapes;

namespace Whiteboard
{
    public partial class Form1 : Form
    {
        public Point currentPoint = new Point();
        public Point oldPoint = new Point();
        public Graphics g;
        public Pen pen = new Pen(Color.Black, 5);
        public Pen penErase = new Pen(Color.White, 20);
        public int width;
        Font myFont = new Font("Arial", 15,FontStyle.Regular);
        SolidBrush myBrush = new SolidBrush(Color.Red);
        TextBox text1 = new TextBox();
        Label label1 = new Label();

        //Bitmap surface;
        //Graphics graph;
        //String sImage = "Picture";
        //int i = 1;

        public Form1()
        {
            InitializeComponent();
            g = panelBoard.CreateGraphics();
            // LineCap and DashCap methods are using System.Drawing.Drawing2D
            pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round); // the line is smoothly that way
            penErase.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
            // Above codes to save our works like image
                //graph = panelBoard.CreateGraphics();
                //surface = new Bitmap(panelBoard.Width, panelBoard.Height);
                //graph = Graphics.FromImage(surface);
                //panelBoard.BackgroundImage = surface;
                //panelBoard.BackgroundImageLayout = ImageLayout.None; 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panelBoard_MouseDown(object sender, MouseEventArgs e)
        {
            oldPoint = e.Location;  
        }

        private void panelBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //currentPoint = e.Location;
                if (btnPen.Checked)
                {
                    currentPoint = e.Location;
                    g.DrawLine(pen, oldPoint, currentPoint);
                    oldPoint = currentPoint;
                }
                
                else if (btnErase.Checked)
                {
                    currentPoint = e.Location;
                    g.DrawLine(penErase, oldPoint, currentPoint);
                    oldPoint = currentPoint;
                }
                //oldPoint = currentPoint;
                //panelBoard.Invalidate();
            }

        }

        private void btnSelect_DoubleClick(object sender, EventArgs e)
        {
            toolStrip1.Width = 30; // set the toolstrip unvisible
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog.Color;
                myBrush.Color = colorDialog.Color;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            text1.Clear();
            panelBoard.Invalidate();
        }

        // When I can save function, other components such as drawing rectangle does not work properly.
        // This way a blank image saved when I clicked save button. I can not handle it.
        private void btnSave_Click(object sender, EventArgs e)
        {
            //surface.Save(sImage, ImageFormat.Png);
            //sImage = "Picture" + i;
            //i++;  find another way to save
        }
        private void panelBoard_MouseUp(object sender, MouseEventArgs e)
        {
            currentPoint = e.Location;
           
            if (btnEllipse.Checked)
            {
                // if I use Math.Abs I could draw the shape from right-bottom to left-top
                g.DrawEllipse(pen, oldPoint.X, oldPoint.Y, Math.Abs(currentPoint.X - oldPoint.X), Math.Abs(currentPoint.Y - oldPoint.Y));
            }
            else if (btnTriangle.Checked)
            {
                Point[] triangle_Point = new Point[3];
                triangle_Point[0] = new Point((oldPoint.X + currentPoint.X) / 2, oldPoint.Y);
                triangle_Point[1] = currentPoint; 
                triangle_Point[2] = new Point(oldPoint.X, currentPoint.Y);// it was needed to triangle's point of left-bottom 

                g.DrawPolygon(pen, triangle_Point);
            }
            else if (btnRectangle.Checked)
            {
                g.DrawRectangle(pen, oldPoint.X, oldPoint.Y, Math.Abs(currentPoint.X-oldPoint.X), Math.Abs(currentPoint.Y - oldPoint.Y));
               
            }
            else if (btnFilledEllipse.Checked)
            {
                g.FillEllipse(myBrush, oldPoint.X, oldPoint.Y, Math.Abs(currentPoint.X - oldPoint.X), Math.Abs(currentPoint.Y - oldPoint.Y));
            }
            else if (btnFilledRectangle.Checked)
            {
                g.FillRectangle(myBrush, oldPoint.X, oldPoint.Y, Math.Abs(currentPoint.X - oldPoint.X), Math.Abs(currentPoint.Y - oldPoint.Y));
            }
            else if (btnFilledTriangle.Checked)
            {
                Point[] triangle_Point = new Point[3];
                triangle_Point[0] = new Point((oldPoint.X + currentPoint.X) / 2, oldPoint.Y);
                triangle_Point[1] = currentPoint; 
                triangle_Point[2] = new Point(oldPoint.X, currentPoint.Y);

                g.FillPolygon(myBrush, triangle_Point);
            }
            else if (btnLine.Checked)
            {
                //LineControl lineControl = new LineControl();
                //lineControl.onPaint(currentPoint);
                g.DrawLine(pen, oldPoint, currentPoint);
            }
            oldPoint = currentPoint;
        }

        private void btnFill_Click(object sender, EventArgs e)
        {

        }

        private void btnText_Click(object sender, EventArgs e)
        {

        }

        private void panelBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                currentPoint = e.Location;
                if (btnText.Checked)
                {
                    // My aim is that user's input display in panel where mouse click point
                    // this scope did not work properly, I can not handle it
                    text1.Location = new Point(e.X, e.Y);
                    label1.Location = currentPoint; 
                    label1.Text = text1.Text;
                    String myString;
                  
                    myString = label1.Text;
                    panelBoard.Controls.Add(text1);
                    panelBoard.Controls.Add(label1);
                    label1.Show();
                } 
            }
        }
        private void text1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                label1.Text = text1.Text;
            }
        }
    }
}
