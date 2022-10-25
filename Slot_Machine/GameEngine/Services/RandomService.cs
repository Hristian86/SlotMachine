using Slot_Machine.GameEngine.Models;
using Slot_Machine.GameEngine.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slot_Machine.GameEngine.Services
{
	internal class RandomService : IRandomService
	{
		private readonly GameSymbols[] percentageBoard;
		private readonly int maxPercentage;

		public RandomService(
            int maxPercentage = 100)
		{
            this.percentageBoard = new GameSymbols[maxPercentage];
			this.maxPercentage = maxPercentage;
		}

        public GameSymbols[] GetPercentageBoard => this.percentageBoard;

		public void FillPercentageBord(List<GameSymbols> addedSymbols)
		{
            for (int i = 0; i < addedSymbols.Count; i++)
            {
                var currentSymbol = addedSymbols[i];

                if (i == addedSymbols.Count - 1)
                {
                    for (int k = 0; k < percentageBoard.Length; k++)
                    {
                        if (this.percentageBoard[k] == null)
                        {
                            this.percentageBoard[k] = currentSymbol;
                        }
                    }

                    break;
                }

                for (int j = 0; j < currentSymbol.PercentLimit; j++)
                {
                    this.AddToBoard(currentSymbol);
                }
            }
        }

        private void AddToBoard(GameSymbols currentSymbol)
        {
            bool added = false;
            while (!added)
            {
                var randomPlace = this.GetRandomNumber(0, (this.maxPercentage - 1));
                if (this.percentageBoard[randomPlace] == null)
                {
                    this.percentageBoard[randomPlace] = currentSymbol;
                    added = true;
                }
            }
        }

        public int GetRandomNumber()
        {
            return new Random().Next(0, this.maxPercentage - 1);
        }

        internal int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}