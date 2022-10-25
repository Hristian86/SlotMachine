using Slot_Machine.GameEngine.Models;
using System.Collections.Generic;

namespace Slot_Machine.Base
{
	internal abstract class CreateSlotSymbolsBase
    {
        protected virtual List<GameSymbols> AddSymbols(List<GameSymbols> symbols)
        {
            symbols.Add(new GameSymbols(
                "Apple", 'A', 45, 0.4f));
            symbols.Add(new GameSymbols(
                "Banana", 'B', 35, 0.6f));
            symbols.Add(new GameSymbols(
                "Pineapple", 'P', 15, 0.8f));
            symbols.Add(new GameSymbols(
                "Wildcard", '*', 5, 0f));

            return symbols;
        }
    }
}