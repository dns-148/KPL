using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class EllipseTool : ToolStripButton, ITool
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private Ellipse EllipseShape;
        private int XPoint;
        private int YPoint;

        public Cursor Cursor
        {
            get
            {
                return Cursors.Arrow;
            }
        }

        public Canvas TargetCanvas
        {
            get
            {
                return this.ActiveCanvas;
            }

            set
            {
                this.ActiveCanvas = value;
            }
        }

        public EllipseTool()
        {
            this.Name = "Ellipse Tool";
            this.ToolTipText = "Ellipse Tool";
            this.Image = Icon.circle;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                XPoint = Event.X;
                YPoint = Event.Y;
                this.EllipseShape = new Ellipse(Event.X, Event.Y);
                this.ActiveCanvas.AddDrawnShape(this.EllipseShape);
            }
        }

        public void ToolMouseMove(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                if (this.EllipseShape != null)
                {
                    int Width = Event.X - this.EllipseShape.X;
                    int Height = Event.Y - this.EllipseShape.Y;

                    if (Width > 0 && Height > 0)
                    {
                        this.EllipseShape.Width = Width;
                        this.EllipseShape.Height = Height;
                    }
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (EllipseShape != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Command = new DrawEllipseCommand(ActiveCanvas, XPoint, YPoint, this.EllipseShape.Width, this.EllipseShape.Height);
                    ActiveCanvas.RemoveDrawnShape(this.EllipseShape);
                    Command.Execute();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Command.UnExecute();
                    Command = null;
                }
            }
        }

        public ICommand GetCommand()
        {
            return Command;
        }

        public void SetCommandNull()
        {
            Command = null;
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {

        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
