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
        public UndeadSmasherObjectFactory objectFactory;

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
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerCamera.transform.forward;
            player.GetComponent<LiveActor>().Attack(direction, 1.0f);
        }
    }
}
