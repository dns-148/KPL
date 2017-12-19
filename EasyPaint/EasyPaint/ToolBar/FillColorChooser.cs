using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using EasyPaint.InterfaceClass;
using EasyPaint.Subject;

namespace EasyPaint.ToolBar
{
    public class FillColorChooser : ToolStripButton, IToolbarItem, IObservable<FillColorSubject>
    {
        private Canvas ActiveCanvas;
        private FillColorSubject FillColor;
        private List<IObserver<FillColorSubject>> Observers;

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
<<<<<<< HEAD
            this.Name = "Outline Color Chooser";
            this.ToolTipText = "Outline Color Chooser";
            Observers = new List<IObserver<FillColorSubject>>();
            FillColor = new FillColorSubject()
            {
                Info = Color.White
            };
            this.BackColor = FillColor.Info;
=======
            this.Name = "Fill Color Chooser";
            this.ToolTipText = "Fill Color Chooser";
            FillColor = Color.White;
            this.BackColor = FillColor;
>>>>>>> 10b8ac524d44c62061dac4f6fe6b52ba4d126556
            this.CheckOnClick = true;
            this.Click += ItemAction;
        }

        public IDisposable Subscribe(IObserver<FillColorSubject> Observer)
        {
            Observers.Add(Observer);
            Observer.OnNext(FillColor);
            return new Unsubscriber<FillColorSubject>(Observers, Observer);
        }

        internal class Unsubscriber<FillColorSubject> : IDisposable
        {
            private List<IObserver<FillColorSubject>> _Observers;
            private IObserver<FillColorSubject> _Observer;

            internal Unsubscriber(List<IObserver<FillColorSubject>> observers, IObserver<FillColorSubject> observer)
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

        public void ItemAction(object Sender, EventArgs Event)
        {
            this.Checked = false;
            ColorDialog PopUp = new ColorDialog();
            if (PopUp.ShowDialog() == DialogResult.OK)
            {
                FillColor.Info = PopUp.Color;
                this.BackColor = FillColor.Info;
                foreach (var Observer in Observers)
                {
                    Observer.OnNext(FillColor);
                }
            }
        }
    }
}
