using EasyPaint.InterfaceClass;
using System.Drawing;

namespace EasyPaint.Commands
{
    public class DrawRectangleCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private Shapes.Rectangle RectangleShape;
        private Color LineColor;
        private Color FillColor;
        private int X;
        private int Y;
        private int Width;
        private int Height;


        public DrawRectangleCommand(Canvas InputCanvas, int XPoint, int YPoint, int Width, int Height)
        {
            this.ActiveCanvas = InputCanvas;
            this.LineColor = ActiveCanvas.LineColor;
            this.FillColor = ActiveCanvas.FillColor;
            this.Width = Width;
            this.Height = Height;
            this.X = XPoint;
            this.Y = YPoint;
        }

        public void Execute()
        {
            RectangleShape = new Shapes.Rectangle(X, Y, Width, Height);
            RectangleShape.SetOutlineColor(LineColor);
            RectangleShape.SetFillColor(FillColor);
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
