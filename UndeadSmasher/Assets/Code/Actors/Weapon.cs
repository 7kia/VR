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

        // Созданной пуле далее присваивается цель
        public GameObject Shoot(float deltatime, Quaternion rotation)
        {
            //Debug.Log("deltatime = " + deltatime);
            //Debug.Log("cooldown.maxTime = " + cooldown.maxTime);
            //Debug.Log("bulletCounter.value = " + bulletCounter.value);
            if (bulletCounter.value > 0)
            {
                cooldown.PlayTimer();
                cooldown.AddToTime(deltatime);
                //Debug.Log("Current time = " + cooldown.time);
                //Debug.Log("Max time = " + cooldown.maxTime);
                //cooldown.AddToTime(deltatime);
                //Debug.Log("cooldown.time = " + cooldown.time);
                //Debug.Log("cooldown.maxTime = " + cooldown.maxTime);
                if (cooldown.NowTimeMoreMax())
                {

                    cooldown.StopTimer();
                    cooldown.ResetTimer();
                    bulletCounter.value -= 1;
                    //Debug.Log("Shoot");
                    //Debug.Log("deltatime = " + deltatime);
                    //Debug.Log("cooldown.time = " + cooldown.time);
                    //Debug.Log("cooldown.maxTime = " + cooldown.maxTime);
                    //Debug.Log("bulletCounter.value = " + bulletCounter.value);
                    //Debug.Log("owner =" + (owner != null));
                    //Debug.Log("bulletOptions =" + (bulletOptions != null));
                    if ((owner == null) || (objectFactory == null))
                    {
                        Debug.Log("objectFactory != null ==> " + (objectFactory != null));
                        Debug.Log("owner != null ==> " + (owner != null));
                        Debug.Log("bulletOptions != null ==> " + (bulletOptions != null));
                    }
                    return objectFactory.CreateObject(owner.transform.position, rotation, bulletOptions.bulletName);
                }
            }
            return null;
        }


    }
}
