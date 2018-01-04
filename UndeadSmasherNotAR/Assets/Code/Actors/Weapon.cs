using Assets.Code.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Actors
{
    public class Weapon : MonoBehaviour
    {
        public BulletOptions bulletOptions = new BulletOptions();// TODO : посмотри как можно занести данные из редактора
        public MyTimer cooldown = new MyTimer();
        public BulletCounter bulletCounter = new BulletCounter();

        public UndeadSmasherObjectFactory objectFactory;
        public GameObject owner;

        void Start()
        {
            cooldown = new MyTimer();
            bulletCounter = new BulletCounter();
            bulletOptions.portionDamage = new PortionDamage();
            bulletOptions.fraction = new Fractions.FractionValue();
        }

        // Созданной пуле далее присваивается цель
        public GameObject Shoot(float deltatime)
        {
            if (bulletCounter.value > 0)
            {
                cooldown.AddToTime(deltatime);
                if (cooldown.NowTimeMoreMax())
                {
                    cooldown.time = 0.0f;
                    bulletCounter.value -= 1;
                    return objectFactory.CreateObject(owner.transform.position, bulletOptions.bulletName);
                }
            }
            return null;
        }


    }
}
