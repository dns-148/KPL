using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyPaint.Subject;
using EasyPaint.InterfaceClass;

namespace EasyPaint
{
    public class HistoryList : ListBox, ICaretaker
    {
        private Canvas ActiveCanvas;
        public Stack<CanvasMomento> MomentoStack;
        private int index;
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

        public HistoryList()
        {
            FormattingEnabled = true;
            Location = new System.Drawing.Point(635, 90);
            Name = "listBox1";
            Size = new System.Drawing.Size(188, 342);
            TabIndex = 0;
            MomentoStack = new Stack<CanvasMomento>();
            index = 0;
        }

        public void SetMomento(CanvasMomento Input)
        {
            index += 1;
            MomentoStack.Push(Input);
            string name = "History " + index.ToString();
            Items.Add(name);
        }

        public CanvasMomento rollBack()
        {
            index -= 1;
            Items.RemoveAt(Items.Count - 1);
            return MomentoStack.Pop();
        }
    }
}
