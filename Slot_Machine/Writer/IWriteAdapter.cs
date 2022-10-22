using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Write
{
    internal interface IWriteAdapter
    {
        void Write<TModel>(TModel message);
    }
}
