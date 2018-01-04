using Assets.Code.Actors;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.GameObjectFactory
{
    public class InanimateActorFactory : MonoBehaviour
    {
        public GameObject prefub;
        public ActorBodyManager actorBodyManager;
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

            ///////////////
            // For model
            BoxCollider actorCollider = newObject.GetComponent<BoxCollider>();
            actorCollider.size = new Vector3(
                float.Parse(otherParameters["sizeX"]),
                float.Parse(otherParameters["sizeY"]),
                float.Parse(otherParameters["sizeZ"])
            );
            actorCollider.center = new Vector3(
                float.Parse(otherParameters["centerX"]),
                float.Parse(otherParameters["centerY"]),
                float.Parse(otherParameters["centerZ"])
            );


            GameObject newModel = Instantiate(
                actorBodyManager.modelDictionary[otherParameters["model"]],
                position,
                Quaternion.Euler(0, 0, 0)
            ) as GameObject;
            newModel.transform.parent = newObject.transform;
            ///////////////

            return newObject;
        }
    }
}
