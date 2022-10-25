using Slot_Machine.GameEngine.Models;
using Slot_Machine.GameEngine.Services.Interfaces;
using Slot_Machine.Write;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.GameEngine.Services
{
	internal class SpinMachineService : ISpinMachineServiceService
	{
		private readonly GameSymbols[,] gameBoard;
		private readonly IRandomService randomService;
		private readonly ISlotSymbolsService slotSymbols;
		private readonly StringBuilder builder;
		private List<int> winIndexes;

		public SpinMachineService(
			int rows, int cols,
			IRandomService randomService,
			ISlotSymbolsService slotSymbols)
		{
			this.builder = new StringBuilder();
			this.gameBoard = new GameSymbols[rows, cols];
			this.randomService = randomService;
			this.slotSymbols = slotSymbols;
			this.winIndexes = new List<int>();
		}

		public GameSymbols[,] GetGameBoard => this.gameBoard;

		public void Run()
		{
			var percentageBoard = randomService.GetPercentageBoard;
			for (int i = 0; i < this.gameBoard.GetLength(0); i++)
			{
				for (int j = 0; j < this.gameBoard.GetLength(1); j++)
				{
					var random = this.randomService.GetRandomNumber();
					this.gameBoard[i, j] = percentageBoard[random];
				}
			}
		}

		public void Display(IWriteAdapter writer)
		{
			for (int i = 0; i < this.gameBoard.GetLength(0); i++)
			{
				for (int j = 0; j < this.gameBoard.GetLength(1); j++)
				{
					this.builder.Append(this.gameBoard[i, j].Symbol);
				}

				this.builder.Append(Environment.NewLine);
			}

			writer.Write(this.builder.ToString());
			this.builder.Clear();
		}

		public double GetCoefficientSum()
		{
			this.CheckForWinnings(this.gameBoard, this.winIndexes);
			
			double coefficient = 0;
			foreach (var winIndex in this.winIndexes)
			{
				for (int i = 0; i < this.gameBoard.GetLength(1); i++)
				{
					coefficient += this.gameBoard[winIndex, i].Coefficient;
				}
			}

			this.winIndexes.Clear();

			return coefficient;
		}

		protected virtual void CheckForWinnings(
			GameSymbols[,] gameBoard,
			List<int> winIdexes)
		{
			for (int i = 0; i < gameBoard.GetLength(0); i++)
			{
				this.WinCheck(gameBoard.GetLength(1), i, gameBoard, winIndexes);
			}
		}

		private void WinCheck(int winRatioRequired, int index, GameSymbols[,] gameBoard, List<int> winIndexes)
		{
			int occurrence = 0;
			var allSymbols = this.slotSymbols.GetSymbols();
			foreach (var currentSymbol in allSymbols)
			{
				for (int k = 0; k < gameBoard.GetLength(1); k++)
				{
					var boardSymbol = gameBoard[index, k];
					if (boardSymbol.Symbol == currentSymbol.Symbol || boardSymbol.Name.Equals("Wildcard", StringComparison.OrdinalIgnoreCase))
					{
						occurrence += 1;
					}
					else
					{
						break;
					}
				}

				if (winRatioRequired == occurrence)
				{
					winIndexes.Add(index);
				}

				occurrence = 0;
			}
		}
	}
}