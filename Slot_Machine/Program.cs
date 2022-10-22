using System;
using Slot_Machine.GameEngine;
using Slot_Machine.Reader;
using Slot_Machine.Reader.Readable;
using Slot_Machine.Write;
using Slot_Machine.Writer.Writable;

namespace Slot_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            // TODO (HK) Build dependancy container IoC and move to another location.
            var consoleReader = new ConsoleReader();
            var reader = new ReadAdapter(consoleReader);

            var consoleWrite = new ConsoleWrite();
            var writer = new WriteAdapter(consoleWrite);
            
            try
            {
                // Dispaly message.
                writer.Write("Please deposit money you would like to play with:");

                decimal depositAmount = reader.Read<decimal>();

                var game = new MainLogic(reader, writer, depositAmount, 4, 3, 4);
                game.Run();

            }
            catch (Exception ex)
            {
                writer.Write(ex.Message);
            }
        }
    }
}
