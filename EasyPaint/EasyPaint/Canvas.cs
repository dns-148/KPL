using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;

namespace EasyPaint
{
    public class Canvas : Control
    {
        private ITool ActiveTool;
        private List<Shape> ShapesDrawn;

        public Canvas()
        {
            this.ShapesDrawn = new List<Shape>();
            this.DoubleBuffered = true;

            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += CanvasPaint;
            this.MouseDown += CanvasMouseDown;
            this.MouseUp += CanvasMouseUp;
            this.MouseMove += CanvasMouseMove;
            this.MouseDoubleClick += CanvasMouseDoubleClick;
            this.KeyDown += CanvasKeyDown;
            this.KeyUp += CanvasKeyUp;
        }

        private void CanvasKeyUp(object Sender, KeyEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolKeyUp(Sender, Event);
            }
        }

        private void CanvasKeyDown(object Sender, KeyEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolKeyDown(Sender, Event);
            }
        }


        private void CanvasMouseDoubleClick(object Sender, MouseEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolMouseDoubleClick(Sender, Event);
                this.CanvasRepaint();
            }
        }

        private void CanvasMouseMove(object Sender, MouseEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolMouseMove(Sender, Event);
                this.CanvasRepaint();
            }
        }

        private void CanvasMouseUp(object Sender, MouseEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolMouseUp(Sender, Event);
                this.CanvasRepaint();
            }
        }

        private void CanvasMouseDown(object Sender, MouseEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolMouseDown(Sender, Event);
                this.CanvasRepaint();
            }
        }

        private void CanvasPaint(object Sender, PaintEventArgs Event)
        {
            foreach (Shape SelectedShape in ShapesDrawn)
            {
                SelectedShape.SetGraphics(Event.Graphics);
                SelectedShape.Draw();
            }
        }

        public void CanvasRepaint()
        {
            this.Invalidate();
            this.Update();
        }

        public void SetActiveTool(ITool Tool)
        {
            this.ActiveTool = Tool;
        }

        public ITool GetActiveTool()
        {
            return this.ActiveTool;
        }

        public void SetBackgroundColor(Color InputColor)
        {
            this.BackColor = InputColor;
        }

        public void AddDrawnShape(Shape SelectedShape)
        {
            this.ShapesDrawn.Add(SelectedShape);
        }

        public void RemoveDrawnShape(Shape SelectedShape)
        {
            this.ShapesDrawn.Remove(SelectedShape);
        }

        public Shape GetShapeAt(int x, int y)
        {
            foreach (Shape SelectedShape in ShapesDrawn)
            {
                if (SelectedShape.Intersect(x, y))
                {
                    return SelectedShape;
                }
            }
            return null;
        }

        public Shape SelectShapeAt(int x, int y)
        {
            Shape SelectedShape = GetShapeAt(x, y);
            if (SelectedShape != null)
            {
                SelectedShape.Select();
            }

            return SelectedShape;
        }

        public void DeselectAllShapes()
        {
            foreach (Shape SelectedShape in ShapesDrawn)
            {
                SelectedShape.Deselect();
            }
        }
    }
}
