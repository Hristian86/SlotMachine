using Slot_Machine.GameEngine.Models;
using Slot_Machine.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.GameEngine.Services.Interfaces
{
	internal interface ISpinMachineServiceService
	{
		void Run();

		GameSymbols[,] GetGameBoard { get; }

		void Display(IWriteAdapter writer);

		double GetCoefficientSum();
	}
}
