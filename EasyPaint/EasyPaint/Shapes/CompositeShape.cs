using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class CompositeShape : Shape
    {
        List<Shape> AllShape;

        public CompositeShape()
        {
            AllShape = new List<Shape>();
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
            foreach(Shape SeletedShape in AllShape)
            {
                SeletedShape.RenderOnNormal();
            }
        }

        public override void RenderOnModify()
        {
            foreach (Shape SeletedShape in AllShape)
            {
                SeletedShape.RenderOnModify();
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            foreach (Shape SeletedShape in AllShape)
            {
                SeletedShape.Translate(x, y, xAmount, yAmount);
            }
        }

        public void Add(Shape SelectedShape)
        {

        }

        public void Remove(Shape SelectedShape)
        {

        }
    }
}
