using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Writer.Writable
{
    internal interface IWriter
    {
        void Write<TModel>(TModel message);
    }
}
