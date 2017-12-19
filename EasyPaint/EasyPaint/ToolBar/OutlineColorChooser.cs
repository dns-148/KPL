using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using EasyPaint.InterfaceClass;
using EasyPaint.Subject;

namespace EasyPaint.ToolBar
{
    public class OutlineColorChooser : ToolStripButton, IToolbarItem, IObservable<LineColorSubject>
    {
        private Canvas ActiveCanvas;
        private LineColorSubject LineColor;
        private List<IObserver<LineColorSubject>> Observers;

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

        public IDisposable Subscribe(IObserver<LineColorSubject> Observer)
        {
            Observers.Add(Observer);
            Observer.OnNext(LineColor);
            return new Unsubscriber<LineColorSubject>(Observers, Observer);
        }

        internal class Unsubscriber<LineColorSubject> : IDisposable
        {
            private List<IObserver<LineColorSubject>> _Observers;
            private IObserver<LineColorSubject> _Observer;

            internal Unsubscriber(List<IObserver<LineColorSubject>> observers, IObserver<LineColorSubject> observer)
            {
                this._Observers = observers;
                this._Observer = observer;
            }

            public void Dispose()
            {
                if (_Observers.Contains(_Observer))
                    _Observers.Remove(_Observer);
            }
        }

        public OutlineColorChooser()
        {
            this.Name = "Outline Color Chooser";
            this.ToolTipText = "Outline Color Chooser";
            Observers = new List<IObserver<LineColorSubject>>();
            LineColor = new LineColorSubject()
            {
                Info = Color.Black
            };
            this.BackColor = LineColor.Info;
            this.CheckOnClick = true;
            this.Click += ItemAction;
        }

        public void ItemAction(object Sender, EventArgs Event)
        {
            this.Checked = false;
            ColorDialog PopUp = new ColorDialog();
            if (PopUp.ShowDialog() == DialogResult.OK)
            {
                LineColor.Info = PopUp.Color;
                this.BackColor = LineColor.Info;
                foreach (var Observer in Observers)
                {
                    Observer.OnNext(LineColor);
                }
            }
        }
    }
}
