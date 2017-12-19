using System;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;

namespace EasyPaint.ToolBar
{
    public class UndoButton : ToolStripButton, IToolbarItem
    {
        private Canvas ActiveCanvas;

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

        public UndoButton()
        {
            this.Name = "Fill Color Chooser";
            this.ToolTipText = "Fill Color Chooser";
            this.Image = Icon.undo256x256;
            this.CheckOnClick = true;
            this.Click += ItemAction;
        }

        public void ItemAction(object Sender, EventArgs Event)
        {
            this.Checked = false;
            ActiveCanvas.Undo();
        }
    }
}
