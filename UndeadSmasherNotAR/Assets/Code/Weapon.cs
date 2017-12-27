using Assets.Code.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class Weapon : MonoBehaviour
    {
        public BulletOptions bulletOptions;// TODO : посмотри как можно занести данные из редактора
        public MyTimer timer;
        public BulletCounter bulletCounter;

        public UndeadSmasherObjectFactory objectFactory;
        public GameObject owner;

        void Start()
        {
            timer = new MyTimer();
            bulletCounter = new BulletCounter();
        }

        public void Shoot(float deltatime)
        {
            timer.AddToTime(deltatime);
            if (timer.NowTimeMoreMax())
            {
                timer.time = 0.0f;
                bulletCounter.value -= 1;
                objectFactory.CreateObject(owner.transform.position, bulletOptions.bulletName);
            }

            
        }


    }
}
