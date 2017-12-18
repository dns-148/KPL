using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using System.Drawing;

namespace EasyPaint.Commands
{
    public class DrawRectangleCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private EasyPaint.Shapes.Rectangle RectangleShape;
        private Color LineColor;
        private int X;
        private int Y;
        private int Width;
        private int Height;


        public DrawRectangleCommand(Canvas InputCanvas, int XPoint, int YPoint, int Width, int Height)
        {
            this.ActiveCanvas = InputCanvas;
            this.LineColor = ActiveCanvas.LineColor;
            this.Width = Width;
            this.Height = Height;
            this.X = XPoint;
            this.Y = YPoint;
        }

        public void Execute()
        {
            RectangleShape = new EasyPaint.Shapes.Rectangle(X, Y, Width, Height);
            RectangleShape.SetOutlineColor(LineColor);
            this.ActiveCanvas.AddDrawnShape(this.RectangleShape);
        }

        public void UnExecute()
        {
            if (RectangleShape != null)
            {
                this.ActiveCanvas.RemoveDrawnShape(this.RectangleShape);
            }
        }
    }
}
