using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;

namespace EasyPaint.Commands
{
    public class DrawRectangleCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private Rectangle RectangleShape;
        private int X;
        private int Y;
        private int Width;
        private int Height;


        public DrawRectangleCommand(Canvas InputCanvas, int XPoint, int YPoint, int Width, int Height)
        {
            this.ActiveCanvas = InputCanvas;
            this.Width = Width;
            this.Height = Height;
            this.X = XPoint;
            this.Y = YPoint;
        }

        public void Execute()
        {
            RectangleShape = new Rectangle(X, Y, Width, Height);
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
