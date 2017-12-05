using System;
using EasyPaint.InterfaceClass;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace EasyPaint.Shapes
{
    public class Line : Shape
    {
        private const double EPSILON = 3.0;

        public Point Startpoint { get; set; }
        public Point Endpoint { get; set; }

        private Pen pen;

        public Line()
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;

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
            pen.Color = Color.Black;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(pen, this.Startpoint, this.Endpoint);
            }
        }

        public override void RenderOnModify()
        {
            pen.Color = Color.Blue;
            pen.Width = 1.5f;
            pen.DashStyle = DashStyle.Solid;

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawLine(pen, this.Startpoint, this.Endpoint);
            }
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

        public override bool Add(Shape SelectedShape)
        {
            return false;
        }

        public override bool Remove(Shape SelectedShape)
        {
            return false;
        }
    }
}
