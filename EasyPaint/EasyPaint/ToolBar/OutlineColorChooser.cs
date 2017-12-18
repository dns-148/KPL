using System;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;
using System.Drawing;

namespace EasyPaint.ToolBar
{
    public class OutlineColorChooser : ToolStripButton, IToolbarItem
    {
        private Canvas ActiveCanvas;
        private Color OutlineColor;

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

        public OutlineColorChooser()
        {
            this.Name = "Outline Color Chooser";
            this.ToolTipText = "Outline Color Chooser";
            OutlineColor = Color.Black;
            this.BackColor = OutlineColor;
            this.CheckOnClick = true;
            this.Click += ItemAction;
        }

        public void ItemAction(object Sender, EventArgs Event)
        {
            this.Checked = false;
            ColorDialog PopUp = new ColorDialog();
            if (PopUp.ShowDialog() == DialogResult.OK)
            {
                OutlineColor = PopUp.Color;
                this.BackColor = OutlineColor;
                this.ActiveCanvas.LineColor = OutlineColor;
            }
        }
    }
}
