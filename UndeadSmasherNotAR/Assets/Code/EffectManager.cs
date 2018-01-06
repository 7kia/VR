using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InteractionTypePair = System.Collections.Generic.KeyValuePair<Assets.Code.Actors.TypeEntity, Assets.Code.Actors.TypeEntity>;
using FractionPair = System.Collections.Generic.KeyValuePair<Assets.Code.Fractions.FractionValue.Fraction, Assets.Code.Fractions.FractionValue.Fraction>;
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
            handlers.Add(new InteractionTypePair(TypeEntity.Bullet, TypeEntity.Inanimate), handleBulletAndInanimate);
            handlers.Add(new InteractionTypePair(TypeEntity.Bullet, TypeEntity.Live), handleBulletAndLive);


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void handleCollision(GameObject first, GameObject second)
        {
            TypeEntity firstType = TypeEntityFunctions.GetType(first);
            TypeEntity secondType = TypeEntityFunctions.GetType(second);

            InteractionTypePair typePair = new InteractionTypePair(firstType, secondType);
            InteractionTypePair reverseTypePair = new InteractionTypePair(secondType, firstType);

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
            Bullet bullet = first.GetComponent<Bullet>();
            InanimateActor inanimateActor = second.GetComponent<InanimateActor>();

            Debug.Log(first.name);
            Debug.Log(bullet != null);
            Debug.Log(second.name);
            Debug.Log(inanimateActor != null);


            if (AggressiveBehavior.CanDestroyBlock(inanimateActor.fraction, bullet.fraction))
            {
                inanimateActor.health.value -= bullet.bulletOptions.portionDamage.damage;
            }
            Destroy(first);
            // Debug.Log("handleBulletAndInanimate");
            //throw new NotImplementedException();
        }

        private void handleBulletAndLive(GameObject first, GameObject second)
        {
            FractionValue.Fraction firstFraction = FractionValue.GetFraction(first);
            FractionValue.Fraction secondFraction = FractionValue.GetFraction(second);

            Bullet bullet = first.GetComponent<Bullet>();
            LiveActor liveActor = second.GetComponent<LiveActor>();

            if (AggressiveBehavior.IsWarringFractions(bullet.fraction, liveActor.fraction))
            {
                liveActor.health.value -= bullet.bulletOptions.portionDamage.damage;
            }
            Destroy(first);

            Debug.Log("handleBulletAndLive");
            // TODO : баг если здоровье = 1 то снаряд противника не срабатывает
            //throw new NotImplementedException();
        }
    }
}