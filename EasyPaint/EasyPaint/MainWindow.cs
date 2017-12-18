using System;
using System.Collections.Generic;
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
            this.MaximizeBox = false;
            SetCanvas();
            SetToolbox();
            InitializeComponent();
        }

        private void SetToolbox()
        {
            ToolBox = new Toolbox();
            ToolBox.SetActiveCanvas(DrawingCanvas);

            LineTool LineToolStrip = new LineTool();
            LineToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            ToolBox.AddTool(LineToolStrip);

            EllipseTool EllipseToolStrip = new EllipseTool();
            EllipseToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            ToolBox.AddTool(EllipseToolStrip);

            RectangleTool RectangleToolStrip = new RectangleTool();
            RectangleToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            ToolBox.AddTool(RectangleToolStrip);
            ToolBox.AddSeparator();

            SelectionTool SelectionToolStrip = new SelectionTool();
            SelectionToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            ToolBox.AddTool(SelectionToolStrip);

            this.Controls.Add(ToolBox);
        }

        private void SetCanvas()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            DrawingCanvas = new Canvas();

            TabPage tabPage = new TabPage(DrawingCanvas.Name);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(tabPage);
            this.tabControl.Location = new System.Drawing.Point(24, 50);
            this.tabControl.Name = DrawingCanvas.Name;
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(600, 400);
            this.tabControl.TabIndex = 3;
            tabPage.Controls.Add((Control)DrawingCanvas);
            this.Controls.Add(this.tabControl);
            this.KeyDown += DrawingCanvas.CanvasKeyDown;
            this.KeyUp += DrawingCanvas.CanvasKeyUp;
        }

        public void Toolbox_ItemClicked(object Sender, EventArgs Event)
        {
            ToolStripButton SelectedTool = (ToolStripButton)Sender;

            if (ActiveTool != SelectedTool)
            {
                foreach (ToolStripItem Tool in ToolBox.Items)
                {
                    if (Tool is ToolStripButton)
                    {
                        ToolStripButton ToolItem = Tool as ToolStripButton;
                        ToolItem.Checked = false;
                    }
                }
                DrawingCanvas.SetActiveTool((ITool)SelectedTool);
                SelectedTool.Checked = true;
                ActiveTool = (ITool)Sender;
            }
            else
            {
                SelectedTool.Checked = false;
                DrawingCanvas.SetActiveTool(null);
                ActiveTool = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Saved");
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Loaded");
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File Exported");
        }
    }
}
