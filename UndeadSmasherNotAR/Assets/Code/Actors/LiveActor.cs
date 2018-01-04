using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Code.Behavior.BulletBehavior;

namespace Assets.Code.Actors
{
    public class LiveActor : Actor
    {
        public enum ActorState
        {
            Shoot,
            NotShoot
        };

        public Weapon weapon;
        public IBehavior behavior;
        public ActorState actorState = ActorState.NotShoot;

        public LiveActor()
        {
            type.value = TypeEntity.Type.Live;
        }

        public void Attack(GameObject target, float deltaTime)
        {
            GameObject createdBullet = weapon.Shoot(deltaTime);
            if (createdBullet)
            {
                Bullet bullet = createdBullet.GetComponent<Bullet>();

                SetTargetForBullet(bullet, target);
            }
            
        }

        private void SetTargetForBullet(Bullet bullet, GameObject target)
        {
            IBehavior bulletBehavior = bullet.bulletOptions.behavior;
            if (bulletBehavior.GetType() == typeof(DirectFlyingBehavior))
            {
                DirectFlyingBehavior directFlyingBehavior = (DirectFlyingBehavior) bulletBehavior;
                directFlyingBehavior.target = target.transform.position;
                Debug.Log("DirectFlyingBehavior");
            }
            else if (bulletBehavior.GetType() == typeof(HomingBehavior))
            {
                HomingBehavior homingBehavior = (HomingBehavior) bulletBehavior;
                homingBehavior.target = target;
                Debug.Log("HomingBehavior");
            }
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (behavior != null)
            {
                behavior.Execute(Time.deltaTime, this.gameObject);
            }
        }
    }
}