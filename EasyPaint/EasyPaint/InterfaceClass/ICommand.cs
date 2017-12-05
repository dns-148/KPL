using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPaint.InterfaceClass
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
