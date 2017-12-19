using EasyPaint.Subject;

namespace EasyPaint.InterfaceClass
{
    public interface ICaretaker
    {
        void SetMomento(CanvasMomento InputMomento);
        CanvasMomento rollBack();
    }
}
