using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Code.Behavior;
using Assets.Code.Fractions;
using Assets.Code.Actors;

namespace Assets.Code.GameObjectFactory
{
    public class BulletFactory : MonoBehaviour
    {
        public GameObject prefub;
        public EffectManager effectManager;
        public GameObject Create(
            Vector3 position,
            FractionValue fraction, 
            Dictionary<string, string> otherParameters
        )
        {
            GameObject newObject = Instantiate(
                prefub,
                position,
                Quaternion.Euler(0, 0, 0)
            ) as GameObject;


            Bullet bullet = newObject.GetComponent<Bullet>();
            bullet.effectManager = effectManager;

            bullet.fraction.value = fraction.value;
            bullet.name = otherParameters["name"];
            bullet.lifeTime = float.Parse(otherParameters["lifeTime"]);
            bullet.velocity = float.Parse(otherParameters["velocity"]);
            bullet.behavior = BehaviorFactory.Create(otherParameters["behavior"]);

            return newObject;
        }

    }
}
