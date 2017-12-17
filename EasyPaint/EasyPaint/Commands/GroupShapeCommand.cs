using EasyPaint.InterfaceClass;
using System.Collections.Generic;
using EasyPaint.Shapes;

namespace EasyPaint.Commands
{
    public class GroupShapeCommand : ICommand
    {
        private Canvas ActiveCanvas;
        private List<Shape> AllShape;
        private CompositeShape GroupShape;

        public GroupShapeCommand(Canvas InputCanvas, List<Shape> ListShape)
        {
            ActiveCanvas = InputCanvas;
            AllShape = new List<Shape>();
            GroupShape = new CompositeShape();
            foreach (Shape SelectedShape in ListShape)
            {
                AllShape.Add(SelectedShape);
                GroupShape.Add(SelectedShape);
            }
        }

        public void Execute()
        {
            ActiveCanvas.DeselectAllShapes();
            foreach (Shape SelectedShape in AllShape)
            {
                ActiveCanvas.RemoveDrawnShape(SelectedShape);
            }
            ActiveCanvas.AddDrawnShape(GroupShape);
            GroupShape.Select();
        }

        public void UnExecute()
        {
            ActiveCanvas.DeselectAllShapes();
            foreach (Shape SelectedShape in AllShape)
            {
                ActiveCanvas.AddDrawnShape(SelectedShape);
            }
            ActiveCanvas.RemoveDrawnShape(GroupShape);
        }
    }
}
