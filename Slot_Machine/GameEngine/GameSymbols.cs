using System;
using System.Collections.Generic;
using System.Text;

namespace Slot_Machine.GameEngine
{
    internal class GameSymbols
    {
        public GameSymbols(
            string name,
            char symbol,
            int percentLimit,
            float coefficient)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.PercentLimit = percentLimit;
            this.Coefficient = coefficient;
        }

        public string Name { get; set; }

        public char Symbol { get; set; }

        public int PercentLimit { get; set; }

        public float Coefficient { get; set; }
    }
}
