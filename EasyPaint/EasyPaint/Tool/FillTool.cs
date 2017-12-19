using EasyPaint.Shapes;
using EasyPaint.Subject;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class FillTool : ToolStripButton, ITool, IObserver<FillColorSubject>
    {
        private Canvas ActiveCanvas;
        private FillColorSubject FillColor;
        private ICommand Command;

        public void OnCompleted()
        {

        }

        public void OnError(Exception E)
        {

        }

        public void OnNext(FillColorSubject NewInfo)
        {
            FillColor = NewInfo;
        }

        public void SubscribeFill(IObservable<FillColorSubject> Provider)
        {
            Provider.Subscribe(this);
        }

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

        public FillTool()
        {
            this.Name = "Fill tool";
            this.ToolTipText = "Fill tool";
            this.Image = Icon.fillicon;
            this.CheckOnClick = true;
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
                if (SelectedShape != null)
                {
                    Command = new ChangeFillColorCommand(ActiveCanvas, FillColor.Info, XPoint, YPoint);
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
