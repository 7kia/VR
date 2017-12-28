using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Fractions
{
    public class FractionValue
    {

        public enum Fraction
        {
            Player,
            Undead,
            Neutral
        }

        public Fraction value = Fraction.Neutral;



    }
}