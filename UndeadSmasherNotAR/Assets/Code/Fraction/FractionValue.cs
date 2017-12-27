using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Fractions
{
    public class FractionValue : MonoBehaviour
    {

        public enum Fraction
        {
            Player,
            Undead,
            Neutral
        }

        public Fraction value = Fraction.Neutral;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}