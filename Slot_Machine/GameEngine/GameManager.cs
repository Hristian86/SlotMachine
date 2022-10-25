using System;
using System.Collections.Generic;
using Slot_Machine.GameEngine.Interfaces;
using Slot_Machine.GameEngine.Services.Interfaces;
using Slot_Machine.Reader;
using Slot_Machine.Write;

namespace Slot_Machine.GameEngine
{
	internal class GameManager : IGameManager
    {
        private readonly IReadAdapter reader;
        private readonly IWriteAdapter writer;
		private readonly ISlotSymbolsService slotSymbols;
		private readonly IRandomService randomService;
		private readonly ISpinMachineServiceService spinService;
		
        private List<int> winIndexes;

        private decimal balance;
        private decimal stake;

		public GameManager(
            IReadAdapter reader,
            IWriteAdapter writer,
            decimal balance,
            ISlotSymbolsService slotSymbols,
            IRandomService randomService,
            ISpinMachineServiceService spinService)
        {
            this.reader = reader;
            this.writer = writer;
            this.balance = Math
                .Floor(balance * 100) / 100;
			this.slotSymbols = slotSymbols;
			this.randomService = randomService;
			this.spinService = spinService;
			this.winIndexes = new List<int>();

            this.FillBoard();
        }

        public void Run()
        {
            this.StartSpins();
        }

        private void SumWins()
        {
            double coefficient = this.spinService.GetCoefficientSum();

            this.writer.Write("");

            if (coefficient > 0)
            {
                // TODO (HK) Move to external service.
                decimal winAmount = (this.stake * (decimal)(Math
                .Floor(coefficient * 10) / 10));

                this.balance += winAmount;
                this.writer.Write($"You have won: {winAmount}");
            }

            this.writer.Write($"Current balance is: {balance}");
        }

        private void FillBoard()
        {
            this.slotSymbols.SetSymbols();

            var addedSymbols = this.slotSymbols.GetSymbols();

            this.randomService.FillPercentageBord(addedSymbols);
        }

        private void StartSpins()
        {
            while (this.balance > 0)
            {
                this.writer.Write("Enter stake amount:");
                this.stake = this.reader.Read<decimal>();
                var amount = this.balance - this.stake;
                if (amount < 0)
                {
                    this.writer.Write("You don't have enough balance");
                    continue;
                }
                else
                {
                    this.balance -= this.stake;

                    this.RunSpinMachine();

                    this.SumWins();
                }
            }
        }

        private void RunSpinMachine()
        {
            this.writer.Write("");
            this.spinService.Run();
            this.spinService.Display(this.writer);
        }
    }
}