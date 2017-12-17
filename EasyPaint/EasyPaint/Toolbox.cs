using System.Windows.Forms;
using EasyPaint.InterfaceClass;

namespace EasyPaint
{
    public class Toolbox : ToolStrip
    {
        private Canvas ActiveCanvas;

        public Toolbox()
        {
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.Location = new System.Drawing.Point(0, 50);
            this.Name = "ToolBox";
            this.Size = new System.Drawing.Size(24, 420);
            this.TabIndex = 0;
            this.Text = "Toolbox";
        }
        
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

        public void AddTool(ITool Tool)
        {
            Tool.TargetCanvas = ActiveCanvas;
            this.Items.Add((ToolStripButton)Tool);
        }
    }
}
