namespace EasyPaint.Tool
{
    public class Toolbox : BaseToolPlace
    {
        public Toolbox()
        {
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.Location = new System.Drawing.Point(0, 50);
            this.Name = "Toolbox";
            this.Size = new System.Drawing.Size(24, 420);
            this.TabIndex = 0;
            this.Text = "Toolbox";
        }        
    }
}
