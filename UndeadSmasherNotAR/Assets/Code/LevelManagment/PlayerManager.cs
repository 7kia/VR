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
        public GameObject playerCamera;
        public GameObject player;
        public GameObject[] weapons;

        public Dictionary<string, Weapon> weaponStorage = new Dictionary<string, Weapon>();

        void Start()
        {
            
        }

        void Update()
        {

        }


        public void SetWeaponStorage()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (!weaponStorage.ContainsKey(weapons[i].name))
                {
                    weaponStorage.Add(weapons[i].name, weapons[i].GetComponent<Weapon>());
                }
            }
        }

        public void ChangeWeapon(string weaponName)
        {
            player.GetComponent<LiveActor>().weapon = weaponStorage[weaponName];
        }

        public void Shoot()
        {
            //player.GetComponent<LiveActor>().weapon.Shoot(1.0f);
        }
    }
}
