using System;
using EasyPaint.InterfaceClass;

public class UndoRedo
{
    private Stack<ICommand> _Undocommands = new Stack<ICommand>();
    private Stack<ICommand> _Redocommands = new Stack<ICommand>();

    private Canvas _Container;
    
    public Canvas Container
    {
        get { return _Container; }
        set { _Container = value; }
    }

    public Canvas Container1 { get => _Container; set => _Container = value; }

    public void Redo(int angka)
    {
        for (int i=1; i<=angka;i++)
        {
            if(__Redocommands.count!=0)
            {
                ICommand command = _Redocommands.Pop();
                command.Execute();
                _Undocommands.Push(command);
            }
        }
    }

}
