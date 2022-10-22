using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Writer.Writable
{
    internal class ConsoleWrite : IWriter
    {
        public void Write<TModel>(TModel message)
        {
            Console.WriteLine(message);
        }
    }
}
