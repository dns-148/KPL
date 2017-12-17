using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyPaint.Shapes
{
    public class CompositeShape : Shape
    {
        List<Shape> GroupShape;
        List<Graphics> GraphicShapes;

        public CompositeShape()
        {
            GroupShape = new List<Shape>();
        }

        public override bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter)
        {
            int NumberofShape = 0;
            int NumberofInsideShape = 0;
            foreach(Shape SelectedShape in GroupShape)
            {
                NumberofShape += 1;
                if(SelectedShape.Inside(xOuter, yOuter, WidthOuter, HeightOuter))
                {
                    NumberofInsideShape += 1;
                }
            }

            if (NumberofShape == NumberofInsideShape)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Intersect(int xTest, int yTest)
        {
            bool IntersectShape = false;
            foreach (Shape SelectedShape in GroupShape)
            {
                if (SelectedShape.Intersect(xTest, yTest))
                {
                    IntersectShape = true;
                    break;
                }
            }

            return IntersectShape;
        }

        public override void RenderOnNormal()
        {
            foreach(Shape SelectedShape in GroupShape)
            {
                SelectedShape.SetGraphics(ShapeGraphics);
                SelectedShape.RenderOnNormal();
            }
        }

        public override void RenderOnModify()
        {
            foreach (Shape SelectedShape in GroupShape)
            {
                SelectedShape.SetGraphics(ShapeGraphics);
                SelectedShape.RenderOnModify();
            }
        }

        public override void Translate(int x, int y, int xAmount, int yAmount)
        {
            foreach (Shape SeletedShape in GroupShape)
            {
                SeletedShape.Translate(x, y, xAmount, yAmount);
            }
        }

        public void Add(Shape SelectedShape)
        {
            GroupShape.Add(SelectedShape);
        }

        public void Remove(Shape SelectedShape)
        {
            GroupShape.Remove(SelectedShape);
        }

        public void RemoveAll()
        {
            GroupShape.Clear();
            GraphicShapes.Clear();
        }
    }
}
