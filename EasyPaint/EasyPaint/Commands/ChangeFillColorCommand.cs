using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using System.Drawing;

namespace EasyPaint.Commands
{
    class ChangeFillColorCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private int XPoint;
        private int YPoint;
        private Color PastColor;
        private Color NextColor;

        public ChangeFillColorCommand(Canvas InputCanvas, Color INextColor, int X, int Y)
        {
            ActiveCanvas = InputCanvas;
            XPoint = X;
            YPoint = Y;
            Shape SelectedShape = ActiveCanvas.GetShapeAt(X, Y);
            PastColor = SelectedShape.GetOutlineColor();
            NextColor = INextColor;
        }

        public void Execute()
        {
            Shape SelectedShape = ActiveCanvas.GetShapeAt(XPoint, YPoint);
            SelectedShape.SetFillColor(NextColor);
        }

        public void UnExecute()
        {
            Shape SelectedShape = ActiveCanvas.GetShapeAt(XPoint, YPoint);
            SelectedShape.SetFillColor(PastColor);
        }
    }
}
