using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rios
{
    public enum FishSpecies
    {
        Tuna,
        ClownFish,
        Blowfish,
        Anglerfish
    }

    public enum Color
    {
        Red,
        Yellow,
        Green,
        Black
    }


    public class Fish
    {
        public double Weight { get; set; }
        public Color Color { get; set; }
        public FishSpecies Species { get; set; }

        public override string ToString()
        {
            return $"{Species}, {Color}, {Weight:#,##0.00}";
        }

        #region Random fish

        private static readonly Random R = new Random();
        private const double MinWeight = 500; // Gramos
        private const double MaxWeight = 5000; // Gramos

        public static Fish RandomFish()
        {
            var fish = new Fish
            {
                Weight = (R.NextDouble() * (MaxWeight - MinWeight)) + MinWeight,
                Species = (FishSpecies) R.Next(4), // There are four species
                Color = (Color) R.Next(4) // There are four colors
            };

            return fish;
        }

        #endregion
    }
}
