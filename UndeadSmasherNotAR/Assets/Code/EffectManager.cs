using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InteractionTypePair = System.Collections.Generic.KeyValuePair<TypeEntity.Type, TypeEntity.Type>;
using FractionPair = System.Collections.Generic.KeyValuePair<Assets.Code.Fractions.FractionValue, Assets.Code.Fractions.FractionValue>;
using Assets.Code.Behavior.BulletBehavior;

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

            Bullet bullet = first.GetComponent<Bullet>();
            LiveActor liveActor = second.GetComponent<LiveActor>();

            if (AggressiveBehavior.IsWarringFractions(bullet.fraction.value, liveActor.fraction.value))
            {
                liveActor.health.value -= bullet.bulletOptions.portionDamage.damage;
            }
            Destroy(first);

            //Debug.Log("handleBulletAndLive");
            //throw new NotImplementedException();
        }
    }
}