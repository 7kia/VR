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
        }


        #region Attack
        public void Attack(Vector3 target, Quaternion rotation, float deltaTime)
        {
            RotateToTarget(target);

            GameObject createdBullet = weapon.Shoot(deltaTime, rotation);
            if (createdBullet)
            {
                SetBulletOption(ref createdBullet, target);
            }
            else
            {
                if (animatior)
                {
                    animatior.SetBool("Attack", true);
                    animatior.SetTrigger(hashToAnimation["Charging"]);
                }
            }

        }

        public void Attack(GameObject target, float deltaTime)
        {
            Attack(target.transform.position, target.transform.rotation, deltaTime);
        }

        private void SetBulletOption(ref GameObject createdBullet, Vector3 target)
        {
            var shift = (target - this.transform.position).normalized * 1.5f;
            createdBullet.transform.position += shift;

            Bullet bullet = createdBullet.GetComponent<Bullet>();
            bullet.fraction = fraction;
            SetTargetForBullet(bullet, target);

            bullet.lifeTimer.StopTimer();
            bullet.lifeTimer.time = 0.0f;
            bullet.lifeTimer.maxTime = weapon.bulletOptions.lifeTime;
            bullet.lifeTimer.PlayTimer();

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

            IBehavior bulletBehavior = bullet.bulletOptions.behavior;
            if (bulletBehavior.GetType() == typeof(DirectFlyingBehavior))
            {
                DirectFlyingBehavior directFlyingBehavior = (DirectFlyingBehavior) bulletBehavior;
                directFlyingBehavior.direction = (target.transform.position - this.transform.position).normalized;
            }
            else if (bulletBehavior.GetType() == typeof(HomingBehavior))
            {
                HomingBehavior homingBehavior = (HomingBehavior) bulletBehavior;
                homingBehavior.target = target;
            }
        }

        // Use this for initialization
        void Start()
        {
        }

        override public void UpdateActor()
        {
            if (behavior != null)
            {
                behavior.Execute(Time.deltaTime, this.gameObject);
            }          
        }
    }
}