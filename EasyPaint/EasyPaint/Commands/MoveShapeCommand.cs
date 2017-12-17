using EasyPaint.InterfaceClass;
using System.Collections.Generic;
using EasyPaint.Shapes;

namespace EasyPaint.Commands
{
    public class MoveShapeCommand : ICommand
    {
        private List<Shape> AllShape;
        int XPoint;
        int YPoint;
        int XDistance;
        int YDistance;

        public MoveShapeCommand(List<Shape> ListShape, int X, int Y, int xAmount, int yAmount)
        {
            XPoint = X;
            YPoint = Y;
            XDistance = xAmount;
            YDistance = yAmount;
            
            AllShape = new List<Shape>();
            foreach (Shape SelectedShape in ListShape)
            {
                AllShape.Add(SelectedShape);
            }
        }

        public void Execute()
        {
            foreach (Shape SelectedShape in AllShape)
            {
                SelectedShape.Translate(XPoint, YPoint, XDistance, YDistance);
            }
        }

        public void UnExecute()
        {
            foreach (Shape SelectedShape in AllShape)
            {
                SelectedShape.Translate(XPoint, YPoint, -1 * XDistance, -1 * YDistance);
            }
        }
    }
}
