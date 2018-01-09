using Assets.Code.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.LevelManagment
{
    public class PlayerManager : MonoBehaviour
    {
        public enum PlayerWeapon
        {
            CobbleWeapon,
            BombWeapon
        }

        public GameObject playerCamera;
        public GameObject player;
        public GameObject directionObject;
        public GameObject[] weapons;
        public UndeadSmasherObjectFactory objectFactory;

        public Dictionary<string, Weapon> weaponStorage = new Dictionary<string, Weapon>();

        void Start()
        {
            
        }

        void Update()
        {
            if(player)
            {
                LiveActor liveActor = player.GetComponent<LiveActor>();
                if(liveActor.isActive)
                {
                    player.transform.position = playerCamera.transform.position;
                    player.transform.rotation = playerCamera.transform.rotation;
                    player.transform.up = playerCamera.transform.up;
                    player.transform.forward = playerCamera.transform.forward;

                    //playerCamera.transform.position = liveActor.transform.position;
                    //playerCamera.transform.rotation = liveActor.transform.rotation;
                    //playerCamera.transform.up = liveActor.transform.up;
                }
            }
        }


        public void SetWeaponStorage()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (!weaponStorage.ContainsKey(weapons[i].name))
                {
                    weaponStorage.Add(weapons[i].name, weapons[i].GetComponent<Weapon>());
                    weaponStorage[weapons[i].name].owner = player;
                    weaponStorage[weapons[i].name].objectFactory = objectFactory;
                }
            }
        }

        public void ChangeWeapon(string weaponName)
        {
            player.GetComponent<LiveActor>().weapon = weaponStorage[weaponName];
        }

        public void Shoot()
        {
            player.GetComponent<LiveActor>().Attack(directionObject.transform.position, 1.0f);
        }

        public bool RemainedBullet()
        {
            for(int i = 0; i < weapons.Length; ++i)
            {
                var weapon = weapons[i].GetComponent<Weapon>();
                if(weapon.bulletCounter.value > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
