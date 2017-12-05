using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class Ellipse : Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Pen pen;

        public Ellipse()
        {
            this.pen = new Pen(Color.Black);
            pen.Width = 1.5f;
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
            this.pen.Color = Color.Black;
            this.pen.DashStyle = DashStyle.Solid;
            Rectangle BoundingRectangle = new Rectangle(X, Y, Width, Height);

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawEllipse(this.pen, BoundingRectangle);
            }
        }

        public override void RenderOnModify()
        {
            this.pen.Color = Color.Blue;
            this.pen.DashStyle = DashStyle.Solid;
            Rectangle BoundingRectangle = new Rectangle(X, Y, Width, Height);

            if (this.GetGraphics() != null)
            {
                this.GetGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.GetGraphics().DrawEllipse(this.pen, BoundingRectangle);
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            this.X += xAmount;
            this.Y += yAmount;
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
