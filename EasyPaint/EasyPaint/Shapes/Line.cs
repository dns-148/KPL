using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class Line : Shape
    {
        private const double EPSILON = 3.0;

        public Point Startpoint { get; set; }
        public Point Endpoint { get; set; }

        private Pen DrawingPen;

        public Line()
        {
            this.DrawingPen = new Pen(Color.Black);
            OutlineColor = Color.Black;
            DrawingPen.Width = 1.5f;
        }

        public void SetColor(Color SelectedColor)
        {
            OutlineColor = SelectedColor;
        }

        public Line(Point InputPoint) : this()
        {
            this.Startpoint = InputPoint;
        }

        public Line(Point StartPoint, Point EndPoint) :this(StartPoint)
        {
            this.Endpoint = EndPoint;
        }

        public override void RenderOnNormal()
        {
            DrawingPen.Color = OutlineColor;
            DrawingPen.Width = 1.5f;
            DrawingPen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(DrawingPen, this.Startpoint, this.Endpoint);
            }
        }

        public override void RenderOnModify()
        {
            DrawingPen.Color = Color.Blue;
            DrawingPen.Width = 1.5f;
            DrawingPen.DashStyle = DashStyle.DashDotDot;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(DrawingPen, this.Startpoint, this.Endpoint);
            }
        }

        public override bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter)
        {
            if(xOuter <= Startpoint.X && xOuter + WidthOuter >= Startpoint.X && yOuter <= Startpoint.Y && yOuter + HeightOuter >= Startpoint.Y)
            {
                if (xOuter <= Endpoint.X && xOuter + WidthOuter >= Endpoint.X && yOuter <= Endpoint.Y && yOuter + HeightOuter >= Endpoint.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool Intersect(int xTest, int yTest)
        {
            double m = GetSlope();
            double b = Endpoint.Y - m * Endpoint.X;
            double y_point = m * xTest + b;

            if (Math.Abs(yTest - y_point) < EPSILON)
            {
                return true;
            }
            return false;
        }

        public double GetSlope()
        {
            double m = (double)(Endpoint.Y - Startpoint.Y) / (double)(Endpoint.X - Startpoint.X);
            return m;
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.Startpoint = new Point(this.Startpoint.X + xAmount, this.Startpoint.Y + yAmount);
            this.Endpoint = new Point(this.Endpoint.X + xAmount, this.Endpoint.Y + yAmount);
        }
    }
}
