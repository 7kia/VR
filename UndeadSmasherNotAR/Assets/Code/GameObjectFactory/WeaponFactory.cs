using Assets.Code.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.GameObjectFactory
{
    public class WeaponFactory : MonoBehaviour
    {
        public GameObject prefub;
        public UndeadSmasherObjectFactory objectFactory;
        public EffectManager effectManager;

        public GameObject Create(
            Vector3 position,
            Dictionary<string, string> otherParameters
        )
        {
            GameObject newObject = Instantiate(
                prefub,
                position,
                Quaternion.Euler(0, 0, 0)
            ) as GameObject;


            Weapon weapon = newObject.GetComponent<Weapon>();

            weapon.name = otherParameters["name"];

            weapon.cooldown.time = 0.0f;
            weapon.cooldown.maxTime = float.Parse(otherParameters["cooldown"]);

            //Debug.Log("weapon.cooldown.time  =" + weapon.cooldown.time);
            //Debug.Log("weapon.cooldown.maxTime =" + weapon.cooldown.maxTime);

            // WARNING : Порядок присваивания важен, смотри реализацию value
            weapon.bulletCounter.maxValue = uint.Parse(otherParameters["bulletCounter__count"]);
            weapon.bulletCounter.value = uint.Parse(otherParameters["bulletCounter__count"]);
            weapon.bulletCounter.isCountless = bool.Parse(otherParameters["bulletCounter__isCountless"]);

            //Debug.Log("weapon.bulletCounter.value =" + weapon.bulletCounter.value);
            //Debug.Log("weapon.bulletCounter.maxValue =" + weapon.bulletCounter.maxValue);
            //Debug.Log("weapon.bulletCounter.isCountless =" + weapon.bulletCounter.isCountless);

            weapon.bulletOptions.bulletName = otherParameters["bulletFeatures__name"];
            weapon.bulletOptions.portionDamage.damage = uint.Parse(otherParameters["bulletFeatures__damage"]);
            weapon.bulletOptions.lifeTime = float.Parse(otherParameters["bulletFeatures__lifeTime"]);
            weapon.bulletOptions.velocity = float.Parse(otherParameters["bulletFeatures__velocity"]);
            weapon.bulletOptions.behavior = objectFactory.behaviorFactory.Create(otherParameters["bulletFeatures__behavior"]);

            return newObject;
        }
    }
}
