﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using EasyPaint.InterfaceClass;
using EasyPaint.Shapes;
using EasyPaint.Subject;

namespace EasyPaint
{
    public class Canvas : Control
    {
        private ITool ActiveTool;
        public ICaretaker Caretaker;
        private Stack<ICommand> UndoStack;
        private Stack<ICommand> RedoStack;
        private List<Shape> ShapesDrawn;
        private List<Shape> memory_stack;
        private Shape temp;

        public List<Shape> GetAllShape()
        {
            return ShapesDrawn;
        }

        public void setCaretaker(ICaretaker Input)
        {
            Caretaker = Input;
        }

        public Canvas()
        {
            this.ShapesDrawn = new List<Shape>();
            this.memory_stack = new List<Shape>();
            this.DoubleBuffered = true;
            this.UndoStack = new Stack<ICommand>();
            this.RedoStack = new Stack<ICommand>();

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

        public void AddCommandtoStack(ICommand InputCommand)
        {
            UndoStack.Push(InputCommand);
        }

        public void Undo()
        {
            var last = ShapesDrawn.Count - 1;
            if (last >= 0)
            {
                this.temp = ShapesDrawn[ShapesDrawn.Count - 1];
                ShapesDrawn.RemoveAt(ShapesDrawn.Count - 1);
                memory_stack.Add(temp);
                Debug.WriteLine("Undo is selected");
                this.CanvasRepaint();
            }

        }

        public void Redo()
        {
            var last = memory_stack.Count - 1;
            if (last >= 0)
            {
                this.temp = memory_stack[memory_stack.Count - 1];
                memory_stack.RemoveAt(memory_stack.Count - 1);
                ShapesDrawn.Add(temp);
                Debug.WriteLine("Redo is selected");
                this.CanvasRepaint();
            }
        }

        public void SetCanvasMomento()
        {
            CanvasMomento momento =  new CanvasMomento(ShapesDrawn, UndoStack, RedoStack);
            Caretaker.SetMomento(momento);
        }

        public void SetState(CanvasMomento Momento)
        {
            ShapesDrawn = new List<Shape>();
            foreach (Shape iter in Momento.GetShapes())
            {
                ShapesDrawn.Add(iter);
            }

            UndoStack = new Stack<ICommand>(Momento.GetUndo());
            RedoStack = new Stack<ICommand>(Momento.GetRedo());
        }   

        public void CanvasKeyUp(object Sender, KeyEventArgs Event)
        {
            if (this.ActiveTool != null)
            {
                this.ActiveTool.ToolKeyUp(Sender, Event);
            }
        }

        public void CanvasKeyDown(object Sender, KeyEventArgs Event)
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

        public List<Shape> GetAllShapesDrawn()
        {
            return this.ShapesDrawn;
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

        public List<Shape> SelectShapeAt(int x, int y, int width, int height)
        {
            List<Shape> SelectedShapes = new List<Shape>();
            foreach (Shape SelectedShape in ShapesDrawn)
            {
                if (SelectedShape.Inside(x, y, width, height))
                {
                    SelectedShape.Select();
                    SelectedShapes.Add(SelectedShape);
                }
            }

            return SelectedShapes;
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
