using EasyPaint.Shapes;
using EasyPaint.Subject;
using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class EllipseTool : ToolStripButton, ITool, IObserver<FillColorSubject>, IObserver<LineColorSubject>
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private Ellipse EllipseShape;
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
                EllipseShape.SetFillColor(FillColor.Info);
                this.ActiveCanvas.AddDrawnShape(this.EllipseShape);
                EllipseShape.Select();
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
                        this.EllipseShape.Width = Width;
                        this.EllipseShape.Height = Height;
                    }
                }
            }
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (EllipseShape != null)
            {
                if (Event.Button == MouseButtons.Left)
                {
                    Command = new DrawEllipseCommand(ActiveCanvas, LineColor.Info, FillColor.Info, XPoint, YPoint, this.EllipseShape.Width, this.EllipseShape.Height);
                    ActiveCanvas.RemoveDrawnShape(this.EllipseShape);
                    ActiveCanvas.AddCommandtoStack(Command);
                    Command.Execute();
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
