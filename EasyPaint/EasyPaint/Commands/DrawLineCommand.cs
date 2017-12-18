using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using System.Drawing;

namespace EasyPaint.Commands
{
    class DrawLineCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private Line LineShape;
        private Color LineColor;
        private int xStartpoint;
        private int yStartpoint;
        private int xEndpoint;
        private int yEndpoint;


        public DrawLineCommand(Canvas canvas, int xStartpoint, int yStartpoint, int xEndpoint, int yEndpoint)
        {
            this.ActiveCanvas = canvas;
            this.LineColor = ActiveCanvas.LineColor;
            this.xEndpoint = xEndpoint;
            this.yEndpoint = yEndpoint;
            this.xStartpoint = xStartpoint;
            this.yStartpoint = yStartpoint;
        }

        public void Execute()
        {
            LineShape = new Line(new System.Drawing.Point(xStartpoint, yStartpoint)){
                Endpoint = new System.Drawing.Point(xEndpoint, yEndpoint)
            };
            LineShape.SetOutlineColor(LineColor);
            ActiveCanvas.AddDrawnShape(LineShape);

        }

        public void UnExecute()
        {
            if (LineShape != null)
            {
                ActiveCanvas.RemoveDrawnShape(LineShape);
            }
        }
    }
}
