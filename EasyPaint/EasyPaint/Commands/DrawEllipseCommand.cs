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


        public DrawEllipseCommand(Canvas InputCanvas, Color ILineColor, Color IFillColor, int XPoint, int YPoint, int Width, int Height)
        {
            this.ActiveCanvas = InputCanvas;
            this.LineColor = ILineColor;
            this.FillColor = IFillColor;
            this.Width = Width;
            this.Height = Height;
            this.X = XPoint;
            this.Y = YPoint;
            EllipseShape = new Ellipse(X, Y, Width, Height);
            EllipseShape.SetOutlineColor(LineColor);
            EllipseShape.SetFillColor(FillColor);
        }

        public void Execute()
        {
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
