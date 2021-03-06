﻿using Assets.Code.Actors;
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

        public static Fraction GetFraction(GameObject gameObject)
        {
            LiveActor liveActor = gameObject.transform.GetComponent<LiveActor>();
            Bullet bullet = gameObject.transform.GetComponent<Bullet>();
            InanimateActor inanimateActor = gameObject.transform.GetComponent<InanimateActor>();

            if (liveActor)
            {
                return liveActor.fraction;
            }
            else if (bullet)
            {
                //Debug.Log(bullet);
                //Debug.Log(bullet.fraction);
                return bullet.fraction;
            }
            else if (inanimateActor)
            {
                return inanimateActor.fraction;
            }

            return Fraction.Neutral;
        }

    }
}