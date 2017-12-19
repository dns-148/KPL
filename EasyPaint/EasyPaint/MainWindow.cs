using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using EasyPaint.InterfaceClass;
using EasyPaint.Tool;
using EasyPaint.ToolBar;
using System.IO;
using System.Text;
using EasyPaint.Shapes;
using EasyPaint.Subject;

namespace EasyPaint
{
    public partial class MainWindow : Form
    {
        private HistoryList History;
        private ITool ActiveTool;
        private Canvas DrawingCanvas;
        private Toolbox ToolBox;
        private Toolbar ToolBar;
        private System.Windows.Forms.TabControl tabControl;

        public MainWindow()
        {
            this.MaximizeBox = false;
            SetCanvas();
            SetTool();
            InitializeComponent();
        }

        private void SetTool()
        {
            // 
            // Toolbar
            // 
            ToolBar = new Toolbar();
            ToolBar.SetActiveCanvas(DrawingCanvas);
            ToolBar.AddSeparator();
            ToolBar.ItemClicked += new ToolStripItemClickedEventHandler(Toolbar_ItemClicked);

            UndoButton ub = new UndoButton();
            ToolBar.AddTool(ub);
            ToolBar.AddSeparator();

            OutlineColorChooser OColorChooser = new OutlineColorChooser();
            ToolBar.AddTool(OColorChooser);
            ToolBar.AddSeparator();

            FillColorChooser IColorChooser = new FillColorChooser();
            ToolBar.AddTool(IColorChooser);
            ToolBar.AddSeparator();

            this.Controls.Add(ToolBar);
            // 
            // Toolbox
            // 
            ToolBox = new Toolbox();
            ToolBox.SetActiveCanvas(DrawingCanvas);

            LineTool LineToolStrip = new LineTool();
            LineToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            LineToolStrip.SubscribeLine(OColorChooser);
            ToolBox.AddTool(LineToolStrip);

            EllipseTool EllipseToolStrip = new EllipseTool();
            EllipseToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            EllipseToolStrip.SubscribeFill(IColorChooser);
            EllipseToolStrip.SubscribeLine(OColorChooser);
            ToolBox.AddTool(EllipseToolStrip);

            RectangleTool RectangleToolStrip = new RectangleTool();
            RectangleToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            RectangleToolStrip.SubscribeFill(IColorChooser);
            RectangleToolStrip.SubscribeLine(OColorChooser);
            ToolBox.AddTool(RectangleToolStrip);
            ToolBox.AddSeparator();

            SelectionTool SelectionToolStrip = new SelectionTool();
            SelectionToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            ToolBox.AddTool(SelectionToolStrip);
            ToolBox.AddSeparator();

            LineFillTool LineFillToolStrip = new LineFillTool();
            LineFillToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            LineFillToolStrip.SubscribeLine(OColorChooser);
            ToolBox.AddTool(LineFillToolStrip);

            FillTool FillToolStrip = new FillTool();
            FillToolStrip.Click += new EventHandler(Toolbox_ItemClicked);
            FillToolStrip.SubscribeFill(IColorChooser);
            ToolBox.AddTool(FillToolStrip);

            this.Controls.Add(ToolBox);
        }

        private void SetCanvas()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            DrawingCanvas = new Canvas();
            DrawingCanvas.Name = "Untitled";
            DrawingCanvas.Text = "Untitled";

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
            //
            // groupControl
            //
            GroupBox HistoryBox = new System.Windows.Forms.GroupBox()
            {
                Location = new System.Drawing.Point(625, 50)
            };
            HistoryBox.Name = "History";
            HistoryBox.Size = new System.Drawing.Size(200, 400);
            HistoryBox.TabIndex = 2;
            HistoryBox.TabStop = false;
            HistoryBox.Text = "History";

            // ListBox
            History = new HistoryList();
            History.TargetCanvas = DrawingCanvas;
            History.MouseDown += History_OnSelected;
            DrawingCanvas.Caretaker = (ICaretaker)History;
            DrawingCanvas.SetCanvasMomento();

            this.Controls.Add(History);
            this.Controls.Add(HistoryBox);
        }

        public void History_OnSelected(object Sender, EventArgs Event)
        {

            int index = History.Items.IndexOf(History.SelectedItem);
            int SumofHistory = History.Items.Count;

            for(int i = SumofHistory - 1; i > 0; i--)
            {
                CanvasMomento momento = History.rollBack();
                if(i == index)
                {
                    DrawingCanvas.SetState(momento);
                    DrawingCanvas.Refresh();
                    History.SetMomento(momento);
                    break;
                }
            }
        }

        public void Toolbar_ItemClicked(object Sender, ToolStripItemClickedEventArgs Event)
        {
            IToolbarItem SelectedToolbarItem = (IToolbarItem)Event.ClickedItem;
            SelectedToolbarItem.TargetCanvas = DrawingCanvas;
        }

        public void Toolbox_ItemClicked(object Sender, EventArgs Event)
        {
            ToolStripButton SelectedTool = (ToolStripButton)Sender;
            DrawingCanvas.DeselectAllShapes();

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
                SelectedTool.Checked = true;
                ITool Selectedtool = (ITool)SelectedTool;
                DrawingCanvas.SetActiveTool(Selectedtool);
                Selectedtool.TargetCanvas = DrawingCanvas;
                ActiveTool = Selectedtool;
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
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    string value;
                    value = "";
                    List<Shape> Shapes= DrawingCanvas.GetAllShapesDrawn();
                    foreach (Shape Shape in Shapes)
                    {
                        //value += Shape;   //yang mau dimasukkin ke teks
                    }
                    byte[] info = new UTF8Encoding(true).GetBytes(value);
                    myStream.Write(info, 0, info.Length);
                    myStream.Close();
                }
            }
            string FileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            DrawingCanvas.Name = FileName;
            DrawingCanvas.Text = FileName;
            MessageBox.Show("File Saved");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true; if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            MessageBox.Show("File Loaded");
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Portable Network Graphic (*.png)|*.png";
            saveFileDialog1.Title = "Export an Document";
            saveFileDialog1.ShowDialog();
            string filename = saveFileDialog1.FileName;
            Bitmap AllImage = new Bitmap(845, 495);
            System.Drawing.Rectangle Background = new System.Drawing.Rectangle(0, 0, 845, 495);
            DrawToBitmap(AllImage, Background);

            Bitmap NewImage = new Bitmap(600, 400);
            using (Graphics UsedGraphic = Graphics.FromImage(NewImage))
            {
                UsedGraphic.DrawImage(AllImage, 0, 0, new System.Drawing.Rectangle(33, 101, 596, 377), GraphicsUnit.Pixel);
            }

            if (filename != "")
            {
                NewImage.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
