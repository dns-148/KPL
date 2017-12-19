using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using EasyPaint.Subject;
using System.Windows.Forms;
using System;

namespace EasyPaint.Tool
{
    public class LineFillTool : ToolStripButton, ITool, IObserver<LineColorSubject>
    {
        private Canvas ActiveCanvas;
        private LineColorSubject LineColor;
        private ICommand Command;

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

        public LineFillTool()
        {
            this.Name = "Line Fill tool";
            this.ToolTipText = "Line Fill tool";
            this.Image = Icon.outlineicon;
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
        }

        public void ToolMouseMove(object Sender, MouseEventArgs Event)
        {
           
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                int XPoint = Event.X;
                int YPoint = Event.Y;
                Shape SelectedShape = ActiveCanvas.GetShapeAt(XPoint, YPoint);
                if(SelectedShape != null)
                {
                    Command = new ChangeOutlineColorCommand(ActiveCanvas, LineColor.Info, XPoint, YPoint);
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
