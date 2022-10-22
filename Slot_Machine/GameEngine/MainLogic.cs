using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Slot_Machine.GameEngine.Interfaces;
using Slot_Machine.Reader;
using Slot_Machine.Write;

namespace Slot_Machine.GameEngine
{
    internal class MainLogic : IMainLogic
    {
        private readonly IReadAdapter reader;
        private readonly IWriteAdapter writer;

        private GameSymbols[] percentageBoard;
        private GameSymbols[] addedSymbols;
        private GameSymbols[,] displayBoard;
        private List<int> winIndexes;

        private decimal balance;
        private decimal stake;

        private int maxSymbols;

        public MainLogic(
            IReadAdapter reader,
            IWriteAdapter writer,
            decimal balance,
            int maxSymbols,
            int displayBoardWidth,
            int displayBoardHeight)
        {
            this.reader = reader;
            this.writer = writer;
            this.balance = Math
                .Floor(balance * 100) / 100;
            this.maxSymbols = maxSymbols;
            this.displayBoard = new GameSymbols[displayBoardHeight, displayBoardWidth];
            this.winIndexes = new List<int>();

            this.FillBoard();
        }

        public void Run()
        {
            this.StartSpins();
        }

        private void SumWins()
        {
            float coefficient = 0;
            foreach (var winIndex in this.winIndexes)
            {
                for (int i = 0; i < this.displayBoard.GetLength(1); i++)
                {
                    coefficient += this.displayBoard[winIndex, i].Coefficient;
                }
            }

            this.writer.Write("");

            if (coefficient > 0)
            {
                decimal winAmount = (this.stake * (decimal)(Math
                .Floor(coefficient * 10) / 10));

                this.balance += winAmount;
                this.writer.Write($"You have won: {winAmount}");
            }

            this.writer.Write($"Current balance is: {balance}");
            this.winIndexes.Clear();
        }

        private void FillBoard()
        {
            this.addedSymbols = this.AddSimbols(this.addedSymbols, this.maxSymbols);

            this.percentageBoard = new GameSymbols[100];

            this.SortSymbolsByPercentage(this, addedSymbols);

            for (int i = 0; i < addedSymbols.Length; i++)
            {
                var currentSymbol = addedSymbols[i];

                // TODO (HK) Move to method or external class.
                if (i == addedSymbols.Length - 1)
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

                // TODO (HK) Move to method or external class.
                for (int j = 0; j < currentSymbol.PercentLimit; j++)
                {
                    this.AddToBoard(currentSymbol);
                }
            }
        }

        private void SortSymbolsByPercentage(MainLogic mainLogic, GameSymbols[] addedSymbols)
        {
            for (int i = 0; i < addedSymbols.Length; i++)
            {
                int lowestPercent = addedSymbols[i].PercentLimit;
                int index = -1;
                for (int j = i; j < addedSymbols.Length; j++)
                {
                    if (lowestPercent >= addedSymbols[j].PercentLimit)
                    {
                        lowestPercent = addedSymbols[j].PercentLimit;
                        index = j;
                    }
                }

                if (index > -1)
                {
                    this.Swap(i, index, addedSymbols);
                }
            }
        }

        private void Swap(int i, int index, GameSymbols[] addedSymbols)
        {
            var oldSymbol = addedSymbols[i];
            addedSymbols[i] = addedSymbols[index];
            addedSymbols[index] = oldSymbol;
        }

        private void AddToBoard(GameSymbols currentSymbol)
        {
            bool added = false;
            while (!added)
            {
                var randomPlace = this.GetRandomNumber(0, 99);
                if (this.percentageBoard[randomPlace] == null)
                {
                    this.percentageBoard[randomPlace] = currentSymbol;
                    added = true;
                }
            }
        }

        private int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        protected virtual GameSymbols[] AddSimbols(
            GameSymbols[] symbols, int maxSymbols)
        {
            symbols = new GameSymbols[maxSymbols];
            symbols[0] = new GameSymbols(
                "Apple", 'A', 45, 0.4f);
            symbols[1] = new GameSymbols(
                "Banana", 'B', 35, 0.6f);
            symbols[2] = new GameSymbols(
                "Pineapple", 'P', 15, 0.8f);
            symbols[3] = new GameSymbols(
                "Wildcard", '*', 5, 0f);

            return symbols;
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
            var bulder = new StringBuilder();
            for (int i = 0; i < this.displayBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.displayBoard.GetLength(1); j++)
                {
                    var random = this.GetRandomNumber(0, 99);
                    this.displayBoard[i, j] = this.percentageBoard[random];
                    bulder.Append(this.displayBoard[i, j].Symbol);
                }

                string symbols = bulder.ToString();
                this.WriteRow(symbols);
                bulder.Clear();

                this.WinCheck(symbols, this.displayBoard.GetLength(1), i);
            }
        }

        private void WinCheck(string symbols, int winRatiorequired, int index)
        {
            int occurrence = 0;
            foreach (var currentSymbol in this.addedSymbols)
            {
                for (int k = 0; k < this.displayBoard.GetLength(1); k++)
                {
                    var boardSymbol = this.displayBoard[index, k];
                    if (boardSymbol.Symbol == currentSymbol.Symbol || boardSymbol.Name.Equals("Wildcard"))
                    {
                        occurrence += 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (winRatiorequired == occurrence)
                {
                    this.winIndexes.Add(index);
                }

                occurrence = 0;
            }
        }

        private void WriteRow(string row)
        {
            this.writer.Write(row);
        }
    }
}