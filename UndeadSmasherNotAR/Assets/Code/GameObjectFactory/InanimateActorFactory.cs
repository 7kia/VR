using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.GameObjectFactory
{
    public class InanimateActorFactory : FactoryWithActorBody
    {
        public GameObject prefub;
        public EffectManager effectManager;

        public GameObject Create(
            Vector3 position,
            Dictionary<string, string> otherParameters
        )
        {
            GameObject newObject = Instantiate(
                prefub,
                position,
                Quaternion.Euler(0, 0, 0)
            ) as GameObject;


            InanimateActor inanimateActor = newObject.GetComponent<InanimateActor>();
            inanimateActor.effectManager = effectManager;

            inanimateActor.name = otherParameters["name"];
            inanimateActor.health.value = uint.Parse(otherParameters["health"]);
            inanimateActor.fraction = FractionFactory.Create(otherParameters["fraction"]);

            SetCollider(otherParameters, newObject);
            CreateModelForActor(newObject, position, otherParameters["model"]);

            return newObject;
        }
    }
}
