﻿using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InteractionTypePair = System.Collections.Generic.KeyValuePair<TypeEntity.Type, TypeEntity.Type>;
using FractionPair = System.Collections.Generic.KeyValuePair<Assets.Code.Fractions.FractionValue, Assets.Code.Fractions.FractionValue>;


namespace Assets.Code
{
    public class EffectManager : MonoBehaviour
    {

        public delegate void EntitiesHandler(GameObject first, GameObject second);

        private Dictionary<InteractionTypePair, EntitiesHandler> handlers = new Dictionary<InteractionTypePair, EntitiesHandler>();
        // Use this for initialization
        void Start()
        {
            handlers.Add(new InteractionTypePair(TypeEntity.Type.Bullet, TypeEntity.Type.Inanimate), handleBulletAndInanimate);
            handlers.Add(new InteractionTypePair(TypeEntity.Type.Bullet, TypeEntity.Type.Live), handleBulletAndLive);


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void handleCollision(GameObject first, GameObject second)
        {
            Actor firstActor = first.GetComponent<Actor>();
            Actor secondActor = second.GetComponent<Actor>();

            TypeEntity firstType = firstActor.type;
            TypeEntity secondType = secondActor.type;

            InteractionTypePair typePair = new InteractionTypePair(firstType.value, secondType.value);
            InteractionTypePair reverseTypePair = new InteractionTypePair(secondType.value, firstType.value);

            if (handlers.ContainsKey(typePair))
            {
                handlers[typePair](first, second);
            }
            else if (handlers.ContainsKey(reverseTypePair))
            {
                handlers[reverseTypePair](second, first);
            }
        }

        void handleBulletAndInanimate(GameObject first, GameObject second)
        {
            Actor firstActor = first.GetComponent<Actor>();
            Actor secondActor = second.GetComponent<Actor>();

            FractionValue firstFraction = firstActor.fraction;
            FractionValue secondFraction = secondActor.fraction;

            // Debug.Log("handleBulletAndInanimate");
            //throw new NotImplementedException();
        }

        private void handleBulletAndLive(GameObject first, GameObject second)
        {
            Actor firstActor = first.GetComponent<Actor>();
            Actor secondActor = second.GetComponent<Actor>();

            FractionValue firstFraction = firstActor.fraction;
            FractionValue secondFraction = secondActor.fraction;

            LiveActor liveActor = first.GetComponent<LiveActor>();
            Bullet bullet = second.GetComponent<Bullet>();

            Debug.Log("liveActor.health.value = " + liveActor.health.value);
            Debug.Log("bullet.bulletOptions.portionDamage.damage = " + bullet.bulletOptions.portionDamage.damage);
            liveActor.health.value -= bullet.bulletOptions.portionDamage.damage;
            Debug.Log("Bullet destroy");
            Destroy(second);

            //Debug.Log("handleBulletAndLive");
            //throw new NotImplementedException();
        }
    }
}