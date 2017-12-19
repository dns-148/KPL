using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using EasyPaint.Subject;
using System;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class RectangleTool : ToolStripButton, ITool, IObserver<FillColorSubject>, IObserver<LineColorSubject>
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private Rectangle RectangleShape;
        private FillColorSubject FillColor;
        private LineColorSubject LineColor;
        private int XPoint;
        private int YPoint;

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

        public void OnNext(LineColorSubject NewInfo)
        {
            LineColor = NewInfo;
        }

        public void SubscribeFill(IObservable<FillColorSubject> Provider)
        {
            Provider.Subscribe(this);
        }

        public void SubscribeLine(IObservable<LineColorSubject> Provider)
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

        public RectangleTool()
        {
            this.Name = "Rectangle Tool";
            this.ToolTipText = "Rectangle Tool";
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
                RectangleShape.SetFillColor(FillColor.Info);
                this.ActiveCanvas.AddDrawnShape(this.RectangleShape);
                RectangleShape.Select();
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

                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (Width > Height)
                        {
                            Width = Height;
                        }
                        else
                        {
                            Height = Width;
                        }
                    }

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
                    Command = new DrawRectangleCommand(ActiveCanvas, LineColor.Info, FillColor.Info, XPoint, YPoint, this.RectangleShape.Width, this.RectangleShape.Height);
                    ActiveCanvas.RemoveDrawnShape(this.RectangleShape);
                    Command.Execute();
                    ActiveCanvas.AddCommandtoStack(Command);
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
