using Slot_Machine.GameEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.GameEngine.Services.Interfaces
{
	internal interface IRandomService
	{
		void FillPercentageBord(List<GameSymbols> addedSymbols);

		GameSymbols[] GetPercentageBoard { get; }

		int GetRandomNumber();
	}
}
