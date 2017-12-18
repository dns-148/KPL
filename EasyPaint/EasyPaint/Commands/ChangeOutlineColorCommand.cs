using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using System.Drawing;

namespace EasyPaint.Commands
{
    public class ChangeOutlineColorCommand: ICommand
    {
        private Canvas ActiveCanvas;
        private int XPoint;
        private int YPoint;
        private Color PastColor;
        private Color NextColor;

        public ChangeOutlineColorCommand(Canvas InputCanvas, int X, int Y)
        {
            ActiveCanvas = InputCanvas;
            XPoint = X;
            YPoint = Y;
            Shape SelectedShape = ActiveCanvas.GetShapeAt(X, Y);
            PastColor = SelectedShape.GetOutlineColor();
            NextColor = ActiveCanvas.LineColor;
        }

        public void Execute()
        {
            Shape SelectedShape = ActiveCanvas.GetShapeAt(XPoint, YPoint);
            SelectedShape.SetOutlineColor(NextColor);
        }

        public void UnExecute()
        {
            Shape SelectedShape = ActiveCanvas.GetShapeAt(XPoint, YPoint);
            SelectedShape.SetOutlineColor(PastColor);
        }
    }
}
