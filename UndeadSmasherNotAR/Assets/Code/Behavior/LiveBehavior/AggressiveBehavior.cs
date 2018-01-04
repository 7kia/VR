using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Behavior.BulletBehavior
{
    class AggressiveBehavior : IBehavior
    {
        public ActorManager actorManager;

        private GameObject target = null;
        private float shortestDistance = float.MaxValue;

        public override void Execute(float deltaTime, GameObject actor)
        {
            GameObject foundTarget = GetTarget(actor);
            if (foundTarget != null)
            {
                actor.GetComponent<LiveActor>().Attack(foundTarget, deltaTime);
            }
            //throw new NotImplementedException("AggressiveBehavior ActorManager");

            //Debug.Log("AggressiveBehavior Execute");
        }

        private GameObject GetTarget(GameObject actor)
        {
            target = null;
            shortestDistance = float.MaxValue;
          
            Transform nodeWithActors = actorManager.scene.transform;
            for (int i = 0; i < nodeWithActors.childCount; i++)
            {
                DefineTarget(nodeWithActors.GetChild(i).gameObject, actor);
            }

            return target;
        }

        private void DefineTarget(GameObject currentChild, GameObject actor)
        {
            FractionValue actorFraction = actor.GetComponent<Actor>().fraction;

            var currentActor = currentChild.GetComponent<Actor>();
            FractionValue currentActorFraction = currentActor.fraction;

            bool noYourself = (currentActor.gameObject != actor);
            bool warringFractions = IsWarringFractions(currentActorFraction.value, actorFraction.value);
            if (noYourself && warringFractions)
            {
                float distance = Vector3.Distance(
                    actor.transform.position,
                    currentActor.transform.position
                );

                shortestDistance = Math.Min(shortestDistance, distance);
                if (shortestDistance == distance)
                {
                    target = currentActor.gameObject;
                }
            }
        }

        private bool IsWarringFractions(FractionValue.Fraction first, FractionValue.Fraction second)
        {
            bool firstIsNeutral = (first == FractionValue.Fraction.Neutral);
            bool secondIsNeutral = (second == FractionValue.Fraction.Neutral);

            if (firstIsNeutral || secondIsNeutral)
            {
                return false;
            }

            return (first != second);
        }

    }
}
