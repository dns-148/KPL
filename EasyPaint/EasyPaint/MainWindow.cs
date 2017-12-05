using System;
using System.Diagnostics;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;
using EasyPaint.Tool;

namespace EasyPaint
{
    public partial class MainWindow : Form
    {
        private ITool ActiveTool;
        private Canvas DrawingCanvas;
        private Toolbox ToolBox;
        private System.Windows.Forms.TabControl tabControl;

        public MainWindow()
        {
            InitializeComponent();
            SetToolbox();
            SetCanvas();
        }

        private void SetToolbox()
        {
          
        }

        private void SetCanvas()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            DrawingCanvas = new Canvas();
            DrawingCanvas.Name = "Untitled";
            TabPage tabPage = new TabPage(DrawingCanvas.Name);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(tabPage);
            this.tabControl.Location = new System.Drawing.Point(30, 50);
            this.tabControl.Name = DrawingCanvas.Name;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(594, 390);
            this.tabControl.TabIndex = 3;
            tabPage.Controls.Add((Control)DrawingCanvas);
            this.Controls.Add(this.tabControl);

        }
    }
}
