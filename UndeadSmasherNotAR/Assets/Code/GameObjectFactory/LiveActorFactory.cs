using Assets.Code.Actors;
using Assets.Code.Behavior;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.GameObjectFactory
{
    public class LiveActorFactory : MonoBehaviour
    {
        public GameObject prefub;
        public UndeadSmasherObjectFactory objectFactory;
        public WeaponFactory weaponFactory;
        public BehaviorFactory behaviorFactory;
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


            LiveActor liveActor = newObject.GetComponent<LiveActor>();
            liveActor.effectManager = effectManager;

            liveActor.name = otherParameters["name"];
            liveActor.health.value = uint.Parse(otherParameters["health"]);
            var fraction = FractionFactory.Create(otherParameters["fraction"]);
            liveActor.fraction = fraction;

            string nameObject = otherParameters["weapon"];
            string newObjectCategory = objectFactory.typeToCategory[nameObject];
            Dictionary<string, string> parametres = objectFactory.actorParameters[newObjectCategory][nameObject];

            liveActor.weapon = weaponFactory.Create(position, parametres).GetComponent<Weapon>();
            liveActor.behavior = BehaviorFactory.Create(otherParameters["behavior"]);

            return newObject;
        }
    }
}
