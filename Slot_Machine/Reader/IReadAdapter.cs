using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Reader
{
    internal interface IReadAdapter
    {
        TModel Read<TModel>();
    }
}
