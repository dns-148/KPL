using EasyPaint.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPaint.Commands
{
    class UndoRedoCommand : ICommand
    {

        private Canvas ActiveCanvas;
        UndoRedoCommand(Canvas canvas)
        {
            this.ActiveCanvas = canvas;
        }
        public void Execute()
        {
            ActiveCanvas.Undo();
        }

        public void UnExecute()
        {
            ActiveCanvas.Redo();
        }
    }
}
