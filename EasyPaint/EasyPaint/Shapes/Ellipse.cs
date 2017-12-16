using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace EasyPaint.Shapes
{
    public class Ellipse : Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Pen DrawingPen;

        public Ellipse()
        {
            this.DrawingPen = new Pen(Color.Black);
            OutlineColor = Color.Black;
            DrawingPen.Width = 1.5f;
        }

        public Ellipse(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public Ellipse(int x, int y, int width, int height) : this(x, y)
        {
            this.Width = width;
            this.Height = height;
        }

        public override bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter)
        {
            if (xOuter <= X && yOuter <= Y && WidthOuter + xOuter >= Width + X && HeightOuter + yOuter >= Height + Y)
            {
                return true;
            }
            return false;
        }

        public override bool Intersect(int xTest, int yTest)
        {
            if ((xTest >= X && xTest <= X + Width) && (yTest >= Y && yTest <= Y + Height))
            {
                return true;
            }
            return false;
        }

        public override void RenderOnNormal()
        {
            DrawingPen.Color = OutlineColor;
            DrawingPen.DashStyle = DashStyle.Solid;
            System.Drawing.Rectangle BoundingRectangle = new System.Drawing.Rectangle(X, Y, Width, Height);

            if (this.GetGraphics() != null)
            {
                GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                GetGraphics().DrawEllipse(DrawingPen, BoundingRectangle);
            }
        }

        public override void RenderOnModify()
        {
            DrawingPen.Color = Color.Blue;
            DrawingPen.DashStyle = DashStyle.DashDotDot;
            System.Drawing.Rectangle BoundingRectangle = new System.Drawing.Rectangle(X, Y, Width, Height);

            if (GetGraphics() != null)
            {
                GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                GetGraphics().DrawEllipse(DrawingPen, BoundingRectangle);
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            X += xAmount;
            Y += yAmount;
        }
    }
}
