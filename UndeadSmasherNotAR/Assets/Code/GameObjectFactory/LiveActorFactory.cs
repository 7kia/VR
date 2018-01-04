﻿using Assets.Code.Actors;
using Assets.Code.Behavior;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.GameObjectFactory
{
    public class LiveActorFactory : FactoryWithActorBody
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

            Debug.Log(prefub.name);
            LiveActor liveActor = newObject.GetComponent<LiveActor>();
            Debug.Log(liveActor != null);
            Debug.Log(otherParameters["name"]);
            liveActor.effectManager = effectManager;

            liveActor.name = otherParameters["name"];
            liveActor.health.value = uint.Parse(otherParameters["health"]);
            liveActor.fraction = FractionFactory.Create(otherParameters["fraction"]);


            string nameObject = otherParameters["weapon"];
            string newObjectCategory = objectFactory.typeToCategory[nameObject];
            Dictionary<string, string> parametres = objectFactory.actorParameters[newObjectCategory][nameObject];

            liveActor.weapon = weaponFactory.Create(position, parametres).GetComponent<Weapon>();
            liveActor.weapon.bulletOptions.fraction = liveActor.fraction;

            var behavior = behaviorFactory.Create(otherParameters["behavior"]);
            //Debug.Log("behavior != null");
            //Debug.Log(behavior != null);
            liveActor.behavior = behaviorFactory.Create(otherParameters["behavior"]);
            //Debug.Log("liveActor.behavior != null");
            //Debug.Log(liveActor.behavior != null);
            
            SetCollider(otherParameters, newObject);
            CreateModelForActor(newObject, position, otherParameters["model"]);

            return newObject;
        }

        private BulletOptions GetBulletOptions(LiveActor actor)
        {
            BulletOptions bulletOptions = new BulletOptions();
            //bulletOptions.behavior;
            //bulletOptions.bulletName = actor.weapon.;
            //bulletOptions.fraction = actor.fraction;
            //bulletOptions.lifeTime


            // TODO : посмотри схему
            return bulletOptions;
        }
    }
}
