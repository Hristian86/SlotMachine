using System;
using System.Collections.Generic;
using System.Text;
using Slot_Machine.Reader.Readable;

namespace Slot_Machine.Reader
{
    internal class ReadAdapter : IReadAdapter
    {
        private readonly IReader reader;

        public ReadAdapter(IReader reader)
        {
            this.reader = reader;
        }

        public TModel Read<TModel>()
        {
            return this.reader.Read<TModel>();
        }
    }
}
