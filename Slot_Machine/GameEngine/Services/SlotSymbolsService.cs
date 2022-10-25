using Slot_Machine.Base;
using Slot_Machine.GameEngine.Models;
using Slot_Machine.GameEngine.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slot_Machine.GameEngine.Services
{
	internal class SlotSymbolsService : CreateSlotSymbolsBase, ISlotSymbolsService
	{
		private List<GameSymbols> stateSymbols;

		public SlotSymbolsService()
		{
			this.stateSymbols = new List<GameSymbols>();
		}

        public void SetSymbols()
		{
            var stateSymbols = base.AddSymbols(this.stateSymbols);
			this.stateSymbols = stateSymbols
				.OrderBy(s => s.PercentLimit)
				.ToList();
		}

		public List<GameSymbols> GetSymbols()
		{
            return this.stateSymbols;
		}
    }
}