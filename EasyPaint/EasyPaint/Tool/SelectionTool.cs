using EasyPaint.InterfaceClass;
using EasyPaint.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using EasyPaint.Shapes;
using System.Windows.Forms;

namespace EasyPaint.Tool
{
    public class SelectionTool : ToolStripButton, ITool
    {
        private Canvas ActiveCanvas;
        private ICommand Command;
        private int XPoint;
        private int YPoint;
        private int xAmount;
        private int yAmount;
        private Selection SelectionArea;
        private List<Shape> SelectedShapes;
        private MovementLine MovementTrace;

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

        public SelectionTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Image = Icon.selectionTool;
            this.CheckOnClick = true;
        }

        public void ToolMouseDown(object Sender, MouseEventArgs Event)
        {
            XPoint = Event.X;
            YPoint = Event.Y;

            if (Event.Button == MouseButtons.Left)
            {
                this.SelectionArea = new Selection(Event.X, Event.Y);
                this.ActiveCanvas.AddDrawnShape(this.SelectionArea);
            }
            else if (Event.Button == MouseButtons.Right)
            {
                bool IntersectShape = false;
                if (SelectedShapes != null)
                {
                    foreach (Shape SelectedShape in SelectedShapes)
                    {
                        if (SelectedShape.Intersect(Event.X, Event.Y))
                        {
                            IntersectShape = true;
                            break;
                        }
                    }
                }

                if(!IntersectShape)
                {
                    SelectedShapes = new List<Shape>();
                    ActiveCanvas.DeselectAllShapes();
                    Shape SelectedShape = ActiveCanvas.GetShapeAt(Event.X, Event.Y);
                    if (SelectedShape != null)
                    {
                        SelectedShape.Select();
                        SelectedShapes.Add(SelectedShape);
                        IntersectShape = true;
                    }
                }

                if (IntersectShape)
                {
                    this.MovementTrace = new MovementLine(new System.Drawing.Point(Event.X, Event.Y))
                    {
                        Endpoint = new System.Drawing.Point(Event.X, Event.Y)
                    };
                    ActiveCanvas.AddDrawnShape(this.MovementTrace);
                }
            }
        }

        public void ToolMouseMove(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                if (this.SelectionArea != null)
                {
                    int Width = Event.X - this.SelectionArea.X;
                    int Height = Event.Y - this.SelectionArea.Y;

                    if (Width > 0 && Height > 0)
                    {
                        this.SelectionArea.Width = Width;
                        this.SelectionArea.Height = Height;
                    }
                }
            }
            else if (Event.Button == MouseButtons.Right)
            {
                this.xAmount = Event.X - XPoint;
                this.yAmount = Event.Y - YPoint;
                if (this.MovementTrace != null)
                {
                    MovementTrace.Endpoint = new System.Drawing.Point(Event.X, Event.Y);
                }
            }
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                Debug.WriteLine("{0} {1} {2} {3}", XPoint, YPoint, Event.X-XPoint, Event.Y - YPoint);
                if (SelectionArea != null)
                {
                    ActiveCanvas.DeselectAllShapes();
                    SelectedShapes = ActiveCanvas.SelectShapeAt(XPoint, YPoint, SelectionArea.Width, SelectionArea.Height);
                    ActiveCanvas.RemoveDrawnShape(SelectionArea);
                }
            }
            else if (Event.Button == MouseButtons.Right)
            {
                ActiveCanvas.RemoveDrawnShape(MovementTrace);
                this.xAmount = Event.X - XPoint;
                this.yAmount = Event.Y - YPoint;
                Command = new MoveShapeCommand(SelectedShapes, XPoint, YPoint, xAmount, yAmount);
                Command.Execute();
                ActiveCanvas.AddCommandtoStack(Command);
                ActiveCanvas.SetCanvasMomento();
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
            if (Event.KeyCode == Keys.G)
            {
                if (SelectedShapes != null && SelectedShapes.Count != 0)
                {
                    Command = new GroupShapeCommand(ActiveCanvas, SelectedShapes);
                    Command.Execute();
                    ActiveCanvas.AddCommandtoStack(Command);
                    SelectedShapes.Clear();
                    ActiveCanvas.SetCanvasMomento();
                }
            }
        }

        public void ToolKeyDown(object Sender, KeyEventArgs Event)
        {

        }
    }
}
