using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;

namespace EasyPaint.Commands
{
    public class DrawEllipseCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private Ellipse EllipseShape;
        private int X;
        private int Y;
        private int Width;
        private int Height;


        public DrawEllipseCommand(Canvas InputCanvas, int XPoint, int YPoint, int Width, int Height)
        {
            this.ActiveCanvas = InputCanvas;
            this.Width = Width;
            this.Height = Height;
            this.X = XPoint;
            this.Y = YPoint;
        }

        public void Execute()
        {
            EllipseShape = new Ellipse(X, Y, Width, Height);
            this.ActiveCanvas.AddDrawnShape(this.EllipseShape);
            EllipseShape.Select();
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
