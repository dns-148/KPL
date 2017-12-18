using System;

namespace EasyPaint.InterfaceClass
{
    public interface IToolbarItem
    {
        Canvas TargetCanvas { get; set; }
        void ItemAction(object Sender, EventArgs Event);
    }
}
