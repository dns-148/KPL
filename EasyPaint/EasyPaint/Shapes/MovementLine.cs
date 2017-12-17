using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class MovementLine : Shape
    {
        public Point Startpoint { get; set; }
        public Point Endpoint { get; set; }

        private Pen DrawingPen;

        public MovementLine()
        {
            this.DrawingPen = new Pen(Color.Red);
            DrawingPen.Width = 1.5f;
            DrawingPen.DashStyle = DashStyle.DashDotDot;
        }

        public MovementLine(Point InputPoint) : this()
        {
            this.Startpoint = InputPoint;
        }

        public MovementLine(Point StartPoint, Point EndPoint) : this(StartPoint)
        {
            this.Endpoint = EndPoint;
        }

        public override void RenderOnNormal()
        {
            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(DrawingPen, this.Startpoint, this.Endpoint);
            }
        }

        public override void RenderOnModify()
        {
           
        }

        public override bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter)
        {
            return false;
        }

        public override bool Intersect(int xTest, int yTest)
        {
            return false;
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.Startpoint = new Point(this.Startpoint.X + xAmount, this.Startpoint.Y + yAmount);
            this.Endpoint = new Point(this.Endpoint.X + xAmount, this.Endpoint.Y + yAmount);
        }
    }
}
