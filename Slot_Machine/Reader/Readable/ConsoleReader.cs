using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.Reader.Readable
{
    internal class ConsoleReader : IReader
    {
        public TModel Read<TModel>()
        {
            string value = Console.ReadLine();

            var result = (TModel)Convert.ChangeType(value, typeof(TModel));

            return result;
        }
    }
}
