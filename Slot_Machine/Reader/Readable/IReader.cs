using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Reader.Readable
{
    internal interface IReader
    {
        TModel Read<TModel>();
    }
}
