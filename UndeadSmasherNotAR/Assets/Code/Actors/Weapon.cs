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
        }

        public void Shoot(float deltatime)
        {
            cooldown.AddToTime(deltatime);
            if (cooldown.NowTimeMoreMax())
            {
                cooldown.time = 0.0f;
                bulletCounter.value -= 1;
                objectFactory.CreateObject(owner.transform.position, bulletOptions.bulletName);
            }

            
        }


    }
}
