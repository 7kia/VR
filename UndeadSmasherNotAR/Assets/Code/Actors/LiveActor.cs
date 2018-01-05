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
            //Debug.Log("Attack");
            GameObject createdBullet = weapon.Shoot(deltaTime);

            if (createdBullet)
            {
                var shift = (target.transform.position - this.transform.position).normalized * 1.5f;
                createdBullet.transform.position += shift;

                Bullet bullet = createdBullet.GetComponent<Bullet>();

                SetTargetForBullet(bullet, target);
            }
            
        }

        private void SetTargetForBullet(Bullet bullet, GameObject target)
        {

            bullet.bulletOptions = weapon.bulletOptions;
            bullet.fraction = weapon.bulletOptions.fraction;
            //Debug.Log("bullet.bulletOptions.lifeTime = " + bullet.bulletOptions.lifeTime);
            //Debug.Log("bullet.bulletOptions.portionDamage.damage = " + bullet.bulletOptions.portionDamage.damage);
            //Debug.Log("bullet.bulletOptions.velocity = " + bullet.bulletOptions.velocity);
            //Debug.Log("bullet.bulletOptions.bulletName = " + bullet.bulletOptions.bulletName);

            IBehavior bulletBehavior = bullet.bulletOptions.behavior;
            if (bulletBehavior.GetType() == typeof(DirectFlyingBehavior))
            {
                DirectFlyingBehavior directFlyingBehavior = (DirectFlyingBehavior) bulletBehavior;
                directFlyingBehavior.direction = (target.transform.position - this.transform.position).normalized;
                //Debug.Log("DirectFlyingBehavior");
            }
            else if (bulletBehavior.GetType() == typeof(HomingBehavior))
            {
                HomingBehavior homingBehavior = (HomingBehavior) bulletBehavior;
                homingBehavior.target = target;
                //Debug.Log("HomingBehavior");
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