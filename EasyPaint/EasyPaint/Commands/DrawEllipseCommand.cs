using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using System.Drawing;

namespace EasyPaint.Commands
{
    public class DrawEllipseCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private Ellipse EllipseShape;
        private Color LineColor;
        private Color FillColor;
        private int X;
        private int Y;
        private int Width;
        private int Height;


        public DrawEllipseCommand(Canvas InputCanvas, int XPoint, int YPoint, int Width, int Height)
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
            EllipseShape = new Ellipse(X, Y, Width, Height);
            EllipseShape.SetOutlineColor(LineColor);
            EllipseShape.SetFillColor(FillColor);
            this.ActiveCanvas.AddDrawnShape(this.EllipseShape);
        }

        public void UnExecute()
        {
            if (EllipseShape != null)
            {
                this.ActiveCanvas.RemoveDrawnShape(this.EllipseShape);
            }
        }
    }
}
