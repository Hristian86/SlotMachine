using Slot_Machine.GameEngine.Models;
using System.Collections.Generic;

namespace Slot_Machine.GameEngine.Services.Interfaces
{
	internal interface ISlotSymbolsService
	{
		void SetSymbols();

		List<GameSymbols> GetSymbols();
	}
}
