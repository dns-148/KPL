using System.Collections.Generic;
using EasyPaint.Shapes;
using EasyPaint.InterfaceClass;

namespace EasyPaint.Subject
{
    public class CanvasMomento
    {
        private List<Shape> AllShape;
        private Stack<ICommand> AllUndo;
        private Stack<ICommand> AllRedo;

        public CanvasMomento(List<Shape> allShape, Stack<ICommand> allUndo, Stack<ICommand> allRedo)
        {
            AllShape = new List<Shape>();
            foreach (Shape iter in allShape)
            {
                AllShape.Add(iter);
            }

            AllUndo = new Stack<ICommand>(allUndo);
            AllRedo = new Stack<ICommand>(allRedo);
            
        }

        public List<Shape> GetShapes()
        {
            return AllShape;
        }

        public Stack<ICommand> GetUndo()
        {
            return AllUndo;
        }

        public Stack<ICommand> GetRedo()
        {
            return AllRedo;
        }
    }
}
