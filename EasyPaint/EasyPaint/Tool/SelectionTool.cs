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
        private int SelectionWidth;
        private int SelectionHeight;
        private Selection SelectionArea;
        private List<Shape> SelectedShapes;

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
            if (Event.Button == MouseButtons.Left)
            {
                XPoint = Event.X;
                YPoint = Event.Y;

                this.SelectionArea = new Selection(Event.X, Event.Y);
                this.ActiveCanvas.AddDrawnShape(this.SelectionArea);
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
        }

        public void ToolMouseUp(object Sender, MouseEventArgs Event)
        {
            if (Event.Button == MouseButtons.Left)
            {
                if (SelectionArea != null)
                {
                    ActiveCanvas.DeselectAllShapes();
                    SelectedShapes = null;
                    SelectionWidth = this.SelectionArea.Width;
                    SelectionHeight = this.SelectionArea.Height;
                    ActiveCanvas.RemoveDrawnShape(this.SelectionArea);
                    SelectedShapes = ActiveCanvas.SelectShapeAt(XPoint, YPoint, SelectionWidth, SelectionHeight);
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
