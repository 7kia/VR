using Assets.Code.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
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

    class Bullet : Actor
    {
        private float velocity;
        private float lifeTime;
        private IBehavior behavior;
        private PortionDamage portionDamage;

        public Bullet()
        {
            type.value = TypeEntity.Type.Bullet;
        }

        void Update()// TODO : может несработать посмотри в код старой игры
        {
            behavior.Execute(Time.deltaTime);
        }
    }
}
