using EasyPaint.Shapes;
using EasyPaint.Subject;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System.Windows.Forms;
using System;

namespace EasyPaint.Tool
{
    public class LineTool : ToolStripButton, ITool, IObserver<LineColorSubject>
    { 
        private Canvas ActiveCanvas;
        private ICommand Command;
        private LineColorSubject LineColor;
        private Line LineShape;
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

        public LineTool()
        {
            this.Name = "Line tool";
            this.ToolTipText = "Line tool";
            this.Image = Icon.line;
            this.CheckOnClick = true;
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception E)
        {

        }

        public void OnNext(LineColorSubject NewInfo)
        {
            LineColor = NewInfo;
        }

        public void SubscribeLine(IObservable<LineColorSubject> Provider)
        {
            Provider.Subscribe(this);
        }

        public void ToolMouseDown(object Sender, MouseEventArgs Event)
        {
            ActiveCanvas.DeselectAllShapes();
            if (Event.Button == MouseButtons.Left)
            {
                XPoint = Event.X;
                YPoint = Event.Y;
                this.LineShape = new Line(new System.Drawing.Point(Event.X, Event.Y))
                {
                    Endpoint = new System.Drawing.Point(Event.X, Event.Y)
                };
                ActiveCanvas.AddDrawnShape(this.LineShape);
                LineShape.Select();
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
                    LineShape = null;
                    Command = new DrawLineCommand(ActiveCanvas, LineColor.Info, XPoint, YPoint, Event.X, Event.Y);
                    Command.Execute();
                    ActiveCanvas.AddCommandtoStack(Command);
                    ActiveCanvas.SetCanvasMomento();
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
