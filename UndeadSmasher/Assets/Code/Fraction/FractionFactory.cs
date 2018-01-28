using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Fractions
{
    public class FractionFactory
    {
        public static FractionValue.Fraction Create(string fractionName)
        {
            switch (fractionName)
            {
                case "Player":
                    return FractionValue.Fraction.Player;
                case "Undead":
                    return FractionValue.Fraction.Undead;
                case "Neutral":
                    return FractionValue.Fraction.Neutral;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
