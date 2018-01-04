using Assets.Code.Component;
using Assets.Code.Fractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Actors
{
    public struct BulletOptions
    {
        public FractionValue fraction;
        public float velocity;
        public float lifeTime;
        public IBehavior behavior;
        public PortionDamage portionDamage;
        public string bulletName;
    }

    public class Bullet : Actor
    {
        public float velocity;
        public float lifeTime;
        public IBehavior behavior;
        public PortionDamage portionDamage;

        public Bullet()
        {
            type.value = TypeEntity.Type.Bullet;
        }

        void Update()// TODO : может несработать посмотри в код старой игры
        {
            if (behavior != null)
            {
                behavior.Execute(Time.deltaTime, this.gameObject);
            }
            
        }
    }
}
