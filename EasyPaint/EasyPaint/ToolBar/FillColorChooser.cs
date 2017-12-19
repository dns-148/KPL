using System;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;
using System.Drawing;

namespace EasyPaint.ToolBar
{
    public class FillColorChooser : ToolStripButton, IToolbarItem
    {
        private Canvas ActiveCanvas;
        public Color FillColor;

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

        public FillColorChooser()
        {
            this.Name = "Fill Color Chooser";
            this.ToolTipText = "Fill Color Chooser";
            FillColor = Color.White;
            this.BackColor = FillColor;
            this.CheckOnClick = true;
            this.Click += ItemAction;
        }

        public void ItemAction(object Sender, EventArgs Event)
        {
            this.Checked = false;
            ColorDialog PopUp = new ColorDialog();
            if (PopUp.ShowDialog() == DialogResult.OK)
            {
                this.FillColor = PopUp.Color;
                this.BackColor = FillColor;
                this.ActiveCanvas.FillColor = this.FillColor;
            }
        }
    }
}
