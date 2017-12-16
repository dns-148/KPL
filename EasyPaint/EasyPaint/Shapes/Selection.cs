using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class Selection : Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Pen DrawingPen;

        public Selection()
        {
            this.DrawingPen = new Pen(Color.Red);
            this.DrawingPen.Width = 1.5f;
            this.DrawingPen.DashStyle = DashStyle.DashDot;
        }

        public Selection(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public Selection(int x, int y, int width, int height) : this(x, y)
        {
            this.Width = width;
            this.Height = height;
        }


        public override bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter)
        {
            return false;
        }

        public override bool Intersect(int xTest, int yTest)
        {
            return false;
        }

        public override void RenderOnNormal()
        {
            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawRectangle(this.DrawingPen, this.X, this.Y, this.Width, this.Height);
            }
        }

        public override void RenderOnModify()
        {
           
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
           
        }
    }
}
