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

            //Debug.Log(prefub.name);
            LiveActor liveActor = newObject.GetComponent<LiveActor>();
            //Debug.Log(liveActor != null);
            //Debug.Log(otherParameters["name"]);
            liveActor.effectManager = effectManager;

            liveActor.name = otherParameters["name"];
            // WARNING : Порядок присваивания важен, смотри реализацию value
            liveActor.health.maxValue = int.Parse(otherParameters["health"]);
            liveActor.health.value = int.Parse(otherParameters["health"]);
            liveActor.fraction = FractionFactory.Create(otherParameters["fraction"]);


            string nameObject = otherParameters["weapon"];
            string newObjectCategory = objectFactory.typeToCategory[nameObject];
            Dictionary<string, string> parametres = objectFactory.actorParameters[newObjectCategory][nameObject];


            var weaponObject = weaponFactory.Create(position, parametres);
            weaponObject.transform.parent = objectFactory.spawnLocations.transform;
            weaponObject.transform.position = position;


            liveActor.weapon = weaponObject.GetComponent<Weapon>();
            liveActor.weapon.owner = newObject;
            liveActor.weapon.objectFactory = objectFactory;
            liveActor.weapon.bulletOptions.fraction = liveActor.fraction;

            //var behavior = behaviorFactory.Create(otherParameters["behavior"]);
            //Debug.Log("behavior != null");
            //Debug.Log(behavior != null);
            liveActor.behavior = behaviorFactory.Create(otherParameters["behavior"]);
            //Debug.Log("liveActor.behavior != null");
            //Debug.Log(liveActor.behavior != null);
            
            SetCollider(otherParameters, newObject);
            CreateModelForActor(newObject, position, otherParameters["model"]);
            LinkLiveActorAndAnimator(ref newObject, ref liveActor);

            return newObject;
        }

        private void LinkLiveActorAndAnimator(ref GameObject gameObject, ref LiveActor liveActor)
        {
            Transform actorModel = gameObject.transform.GetChild(0);

            liveActor.animatior = actorModel.GetComponent<Animator>();
            liveActor.hashToAnimation.Add("Charging", Animator.StringToHash("Charging"));
            liveActor.hashToAnimation.Add("Idle", Animator.StringToHash("Idle"));
        }
        //private BulletOptions GetBulletOptions(LiveActor actor)
        //{
        //    BulletOptions bulletOptions = new BulletOptions();
        //    //bulletOptions.behavior;
        //    //bulletOptions.bulletName = actor.weapon.;
        //    //bulletOptions.fraction = actor.fraction;
        //    //bulletOptions.lifeTime


        //    // TODO : посмотри схему
        //    return bulletOptions;
        //}
    }
}
