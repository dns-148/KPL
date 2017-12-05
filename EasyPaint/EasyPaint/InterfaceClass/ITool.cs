using System.Windows.Forms;

namespace EasyPaint.InterfaceClass
{
    public interface ITool
    {
        Canvas TargetCanvas { get; set; }
        Cursor Cursor { get; }
        void ToolMouseDown(object Sender, MouseEventArgs Event);
        void ToolMouseUp(object Sender, MouseEventArgs Event);
        void ToolMouseMove(object Sender, MouseEventArgs Event);
        void ToolMouseDoubleClick(object Sender, MouseEventArgs Event);
        ICommand GetCommand();
        void SetCommandNull();
        void ToolKeyUp(object Sender, KeyEventArgs Event);
        void ToolKeyDown(object Sender, KeyEventArgs Event);
    }
}
