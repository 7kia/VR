using Assets.Code.Component;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Actors
{
    public class BulletOptions
    {
        public FractionValue.Fraction fraction;
        public float velocity;
        public float lifeTime;
        public IBehavior behavior;
        public PortionDamage portionDamage = new PortionDamage();
        public string bulletName;

        public BulletOptions()
        {

        }

    }

    public class Bullet : Actor
    {
        public BulletOptions bulletOptions = new BulletOptions();
        public MyTimer lifeTimer = new MyTimer();
        public Bullet()
        {
            type = TypeEntity.Bullet;

        }

        void Update()// TODO : может несработать посмотри в код старой игры
        {
            if (isActive)
            {
                UpdateBehavior();
                lifeTimer.AddToTime(Time.deltaTime);
            }
            else
            {
                lifeTimer.StopTimer();
            }

        }

        private void UpdateBehavior()
        {
            if (bulletOptions.behavior != null)
            {
                bulletOptions.behavior.Execute(Time.deltaTime, this.gameObject);
            }
        }
    }
}
