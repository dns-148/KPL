using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System.Diagnostics;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class LineTool : ToolStripButton, ITool
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private Line LineShape;
        private int xStartpoint;
        private int yStartpoint;

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

        public LineTool()
        {
            this.Name = "Line tool";
            this.ToolTipText = "Line tool";
            this.Image = Icon.line;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                //Debug.WriteLine("Event Line Tool");
                xStartpoint = Event.X;
                yStartpoint = Event.Y;
                LineShape = new Line(new System.Drawing.Point(Event.X, Event.Y));
                LineShape.Endpoint = new System.Drawing.Point(Event.X, Event.Y);
                ActiveCanvas.AddDrawnShape(LineShape);
            }
        }

        public void ToolMouseMove(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                if (this.LineShape != null)
                {
                    LineShape.Endpoint = new System.Drawing.Point(Event.X, Event.Y);
                }
            }
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (this.LineShape != null)
            {
                if (Event.Button == MouseButtons.Left)
                {
                    ActiveCanvas.RemoveDrawnShape(this.LineShape);
                    Command = new DrawLineCommand(ActiveCanvas, xStartpoint, yStartpoint, Event.X, Event.Y);
                    Command.Execute();
                }
            }
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

        public ICommand GetCommand()
        {
            return this.Command;
        }

        public void SetCommandNull()
        {
            this.Command = null;
        }
    }
}
