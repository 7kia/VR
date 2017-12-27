using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Fractions
{
    public class FractionFactory
    {
        public static FractionValue Create(string fractionName)
        {
            FractionValue product = new FractionValue();
            switch (fractionName)
            {
                case "Player":
                    product.value = FractionValue.Fraction.Player;
                    break;
                case "Undead":
                    product.value = FractionValue.Fraction.Undead;
                    break;
                case "Neutral":
                    product.value = FractionValue.Fraction.Neutral;
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            return product;
        }
    }
}
