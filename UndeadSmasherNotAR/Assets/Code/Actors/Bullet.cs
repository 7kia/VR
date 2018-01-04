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
        public FractionValue fraction = new FractionValue();
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
        public float velocity;
        public float lifeTime;
        public IBehavior behavior;
        public PortionDamage portionDamage;
        public BulletOptions bulletOptions;

        public Bullet()
        {
            bulletOptions = new BulletOptions();
            bulletOptions.portionDamage = new PortionDamage();
            bulletOptions.fraction = new FractionValue();

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
