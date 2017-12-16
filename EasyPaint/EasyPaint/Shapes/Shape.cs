using EasyPaint.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace EasyPaint.Shapes
{
    public abstract class Shape
    {
        public Guid ID { get; set; }
        public Color OutlineColor { get; set; }
        public Color FillColor { get; set; }

        public BasicState State
        {
            get
            {
                return this.ShapeState;
            }
        }

        private BasicState ShapeState;
        private Graphics ShapeGraphics;

        public Shape()
        {
            ID = Guid.NewGuid();
            this.ChangeState(NormalState.GetInstance());
        }

        public abstract bool Intersect(int xTest, int yTest);
        public abstract bool Inside(int xOuter, int yOuter, int WidthOuter, int HeightOuter);
        public abstract void Translate(int x, int y, int xAmount, int yAmount);

        public abstract void RenderOnModify();
        public abstract void RenderOnNormal();

        public void ChangeState(BasicState InputState)
        {
            this.ShapeState = InputState;
        }

        public virtual void Draw()
        {
            this.ShapeState.Draw(this);
        }

        public virtual void SetGraphics(Graphics InputGraphics)
        {
            this.ShapeGraphics = InputGraphics;
        }

        public virtual Graphics GetGraphics()
        {
            return this.ShapeGraphics;
        }

        public void Select()
        {
            this.ShapeState.Select(this);
        }

        public void Deselect()
        {
            this.ShapeState.Deselect(this);
        }
    }
}
