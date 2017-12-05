using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPaint.Shapes;

namespace EasyPaint.State
{
    public abstract class BasicState
    {
        /*public BasicState State
        {
            get
            {
                return this.ShapeState;
            }
        }*/

        //private BasicState ShapeState;
        
        public virtual void Draw(Shape SelectedShape) { }
        public virtual void Deselect(Shape SelectedShape) { }
        public virtual void Select(Shape SelectedShape) { }
    }
}
