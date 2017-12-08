using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class RectangleTool : ToolStripButton, ITool
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private Rectangle RectangleShape;
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

        public RectangleTool()
        {
            this.Name = "Ellipse Tool";
            this.ToolTipText = "Ellipse Tool";
            this.Image = Icon.rectangle;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                XPoint = Event.X;
                YPoint = Event.Y;
                this.RectangleShape = new Rectangle(Event.X, Event.Y);
                this.ActiveCanvas.AddDrawnShape(this.RectangleShape);
            }
        }

        public void ToolMouseMove(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                if (this.RectangleShape != null)
                {
                    int Width = Event.X - this.RectangleShape.X;
                    int Height = Event.Y - this.RectangleShape.Y;

                    if (Width > 0 && Height > 0)
                    {
                        this.RectangleShape.Width = Width;
                        this.RectangleShape.Height = Height;
                    }
                }
            }
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (RectangleShape != null)
            {
                if (Event.Button == MouseButtons.Left)
                {
                    Command = new DrawRectangleCommand(ActiveCanvas, XPoint, YPoint, this.RectangleShape.Width, this.RectangleShape.Height);
                    ActiveCanvas.RemoveDrawnShape(this.RectangleShape);
                    Command.Execute();
                }
                else if (Event.Button == MouseButtons.Right)
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

        public void ToolMouseDoubleClick(object Sender, MouseEventArgs Event)
        {

        }

        public void ToolKeyUp(object Sender, KeyEventArgs Event)
        {

        }

        public void ToolKeyDown(object Sender, KeyEventArgs Event)
        {

        }
    }
}
