using System;
using Slot_Machine.GameEngine;
using Slot_Machine.GameEngine.Interfaces;
using Slot_Machine.GameEngine.Services;
using Slot_Machine.GameEngine.Services.Interfaces;
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

		// TODO (HK) Move to external manager.
		private static void Start()
		{
			// TODO (HK) Build dependancy container IoC and move to another location.
			IReader consoleReader = new ConsoleReader();
			IReadAdapter reader = new ReadAdapter(consoleReader);

			IWriter consoleWrite = new ConsoleWrite();
			IWriteAdapter writer = new WriteAdapter(consoleWrite);

			IRandomService randomService = new RandomService(maxPercentage: 100);
			ISlotSymbolsService slotService = new SlotSymbolsService();
			ISpinMachineServiceService spinMachineService = new SpinMachineService(rows: 4, cols: 3, randomService, slotService);

			try
			{
				// Dispaly message.
				writer.Write("Please deposit money you would like to play with:");

				decimal depositAmount = reader.Read<decimal>();

				IGameManager game = new GameManager(
					reader,
					writer,
					depositAmount,
					slotService,
					randomService,
					spinMachineService);

				game.Run();

			}
			catch (Exception ex)
			{
				writer.Write(ex.Message);
			}
		}
	}
}
