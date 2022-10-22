using Slot_Machine.Writer.Writable;

namespace Slot_Machine.Write
{
    internal class WriteAdapter : IWriteAdapter
    {
        private readonly IWriter writer;

        public WriteAdapter(IWriter writer)
        {
            this.writer = writer;
        }

        public void Write<TModel>(TModel message)
        {
            this.writer.Write(message);
        }
    }
}
