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
        }

        // Созданной пуле далее присваивается цель? поэтому её возвращаем
        public GameObject Shoot(float deltatime, Quaternion rotation)
        {
            if (bulletCounter.value > 0)
            {
                cooldown.PlayTimer();
                cooldown.AddToTime(deltatime);

                if (cooldown.NowTimeMoreMax())
                {
                    cooldown.StopTimer();
                    cooldown.ResetTimer();
                    bulletCounter.value -= 1;

                    return objectFactory.CreateObject(owner.transform.position, rotation, bulletOptions.bulletName);
                }
            }
            return null;
        }


    }
}
