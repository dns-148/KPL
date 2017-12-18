using System.Windows.Forms;
using EasyPaint.InterfaceClass;

namespace EasyPaint
{
    public class BaseToolPlace : ToolStrip
    {
        private Canvas ActiveCanvas;

        public void SetActiveCanvas(Canvas SelectedCanvas)
        {
            ActiveCanvas = SelectedCanvas;
        }

        public void AddSeparator()
        {
            ToolStripSeparator Separator = new System.Windows.Forms.ToolStripSeparator()
            {
                Size = new System.Drawing.Size(21, 6)
            };

            this.Items.Add(new System.Windows.Forms.ToolStripSeparator());
        }

        public void AddTool(ToolStripButton Tool)
        {
            this.Items.Add(Tool);
        }
    }
}
