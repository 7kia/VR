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
        public Animator animatior;

        public Dictionary<string, int> hashToAnimation = new Dictionary<string, int>();

        public LiveActor()
        {
            type = TypeEntity.Live;
            //animations = GetComponent<Animator>();
        }


        #region Attack
        public void Attack(Vector3 target, Quaternion rotation, float deltaTime)
        {
            if (isActive)
            {
                RotateToTarget(target);

                GameObject createdBullet = weapon.Shoot(deltaTime, rotation);
                if (createdBullet)
                {
                    SetTargetOption(ref createdBullet, target);
                }
                else
                {
                    if (animatior)
                    {
                        animatior.SetBool("Attack", true);
                        //animatior.SetTrigger(hashToAnimation["Charging"]);
                    }
                }
            }         
        }

        public void Attack(GameObject target, float deltaTime)
        {
            Attack(target.transform.position, target.transform.rotation, deltaTime);
        }

        private void SetTargetOption(ref GameObject createdBullet, Vector3 target)
        {
            var shift = (target - this.transform.position).normalized * 1.5f;
            createdBullet.transform.position += shift;

            Bullet bullet = createdBullet.GetComponent<Bullet>();
            bullet.fraction = fraction;
            SetTargetForBullet(bullet, target);
        }

        private void SetTargetForBullet(Bullet bullet, Vector3 target)
        {
            bullet.bulletOptions = weapon.bulletOptions;
            bullet.fraction = weapon.bulletOptions.fraction;


            IBehavior bulletBehavior = bullet.bulletOptions.behavior;
            if (bulletBehavior.GetType() == typeof(DirectFlyingBehavior))
            {
                DirectFlyingBehavior directFlyingBehavior = (DirectFlyingBehavior)bulletBehavior;
                directFlyingBehavior.direction = (target - this.transform.position).normalized;
                //Debug.Log("DirectFlyingBehavior");
            }
        }
        #endregion

        public void Idle()
        {
            if (animatior)
            {
                animatior.SetBool("Attack", false);
                //animatior.SetTrigger(hashToAnimation["Idle"]);
            }
        }

        public void RotateToTarget(Vector3 target)
        {
            transform.LookAt(target);
            var newQuatrnion = transform.rotation;
            newQuatrnion.x = 0.0f;
            newQuatrnion.z = 0.0f;

            transform.rotation = newQuatrnion;
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
            if(isActive)
            {
                if (behavior != null)
                {
                    behavior.Execute(Time.deltaTime, this.gameObject);
                }
            }
           
        }
    }
}