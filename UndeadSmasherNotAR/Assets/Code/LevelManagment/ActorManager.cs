﻿using Assets.Code;
using Assets.Code.Actors;
using Assets.Code.LevelManagment;
using UnityEngine;
using System;
using Assets.Code.Fractions;
using System.Collections.Generic;

public class ActorManager : MonoBehaviour {

    public EffectManager effectManager = null;
    public PlayerManager playerManager = null;
    public GameObject scene = null;
    public UndeadSmasherObjectFactory objectFactory = null;

    private int magicGeneratorIndex = 0;

    public bool checkActors = false;
    public static string MAGIC_GENERATOR_NAME = "MagicGenerator";
    public static string PLAYER_ENTITY_NAME = "MagicEye";

    public Dictionary<PlayerManager.PlayerWeapon, string> WEAPON_NAMES = new Dictionary<PlayerManager.PlayerWeapon, string>();


    public ActorManager()
    {
        WEAPON_NAMES.Add(PlayerManager.PlayerWeapon.CobbleWeapon, "PlayerCobbleWeapon");
        WEAPON_NAMES.Add(PlayerManager.PlayerWeapon.BombWeapon, "PlayerBombWeapon");
    }
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		if (checkActors)
        {
            CheckHealth();
        }
	}

    public void FindMagicGenerator()
    {
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            if (child)
            {
                if(child.name == MAGIC_GENERATOR_NAME)
                {
                    magicGeneratorIndex = i;
                    return;
                }
            }
        }
        throw new Exception("Magic generator not found");
    }

    #region GeneratePlayer
    public void GeneratePlayer()
    {
        Vector3 playerPosition = playerManager.playerCamera.transform.position;

        RecreatePlayer(playerPosition);
        RecreateWeaponStorage(playerPosition);
        ResetPlayerWeapon();
    }

    private void RecreatePlayer(Vector3 playerPosition)
    {
        playerManager.player = null;
        playerManager.player = objectFactory.CreateObject(playerPosition, new Quaternion(), PLAYER_ENTITY_NAME);
    }

    #region RecreateWeaponStorage
    private void RecreateWeaponStorage(Vector3 playerPosition)
    {
        ResetWeaponStorage();
        PlayerManager.PlayerWeapon[] weaponNames = new PlayerManager.PlayerWeapon[2]
        {
             PlayerManager.PlayerWeapon.CobbleWeapon,
             PlayerManager.PlayerWeapon.BombWeapon
        };
        // WARNING : не забудь проверить кол-во оружия
        // TODO : после переигрывания не работает оружие, нужно ссылаться по индексу, а не по указателю
        CreatePlayerWeapons(weaponNames, playerPosition);
        SetPlayerWeaponOptions();
    }

    private void ResetWeaponStorage()
    {
        for (int i = 0; i < WEAPON_NAMES.Count; i++)
        {
            playerManager.weapons[i] = null;
        }
    }

    public void CreatePlayerWeapons(PlayerManager.PlayerWeapon[] weaponNames, Vector3 playerPosition)
    {
        for (uint i = 0; i < WEAPON_NAMES.Count; ++i)
        {
            Debug.Log("WEAPON_NAMES " + WEAPON_NAMES.Count);
        }

        for (uint i = 0; i < weaponNames.Length; ++i)
        {
            Debug.Log(weaponNames[i]);
            playerManager.weapons[i] = objectFactory.CreateObject(playerPosition, new Quaternion(), WEAPON_NAMES[weaponNames[i]]);
        }
    }

    private void SetPlayerWeaponOptions()
    {
        for (int i = 0; i < WEAPON_NAMES.Count; i++)
        {
            Weapon weapon = playerManager.weapons[i].GetComponent<Weapon>();
            LiveActor liveActor = playerManager.player.GetComponent<LiveActor>();
            weapon.owner = playerManager.player;
            weapon.objectFactory = objectFactory;
            weapon.bulletOptions.fraction = liveActor.fraction;
        }
    }
    #endregion

    private void ResetPlayerWeapon()
    {
        LiveActor player = playerManager.player.GetComponent<LiveActor>();
        Debug.Log("playerManager.player.GetComponent<Weapon>().owner " + (player.weapon.owner != null));
        player.weapon = playerManager.weapons[0].GetComponent<Weapon>();
        player.weapon.owner = playerManager.player;

        Debug.Log("player.weapon.owner " + (player.weapon.owner));

    }

    #endregion

    public uint GetAward()
    {
        uint award = 0;
        if(!MagicGeneratorIsLive())
        {
            award++;
            if (!ContentUndead())
            {
                award++;
            }
            if(playerManager.RemainedBullet())
            {
                award++;
            }
            return award;
        }
        else
        {
            throw new Exception("Award not give if magic generator not destroy");
        }

    }

    #region ClearScene
    public void ClearScene()
    {
        
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            if (child)
            {
                ClearChilds(child);

                Destroy(child);
            }
        }
        magicGeneratorIndex = -1;
    }

    private void ClearChilds(GameObject node)
    {
        Weapon weapon = node.GetComponent<Weapon>();
        if (weapon)
        {
            ClearWeapon(ref weapon);
        }

        LiveActor liveActor = node.GetComponent<LiveActor>();
        if(liveActor)
        {
            ClearLiveActor(ref liveActor);
        }
    }

    private void ClearWeapon(ref Weapon weapon)
    {
        weapon.owner = null;
        weapon.objectFactory = null;
    }

    private void ClearLiveActor(ref LiveActor actor)
    {
        ClearWeapon(ref actor.weapon);
    }
    #endregion


    public bool MagicGeneratorIsLive()
    {
        //Debug.Log("magicGenerator health =" + scene.transform.GetChild(magicGeneratorIndex).GetComponent<LiveActor>().health.value);
        return scene.transform.GetChild(magicGeneratorIndex).GetComponent<LiveActor>().health.value > 0;
    }

    public bool ContentUndead()
    {
        bool isUndead = false;
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;

            if (child.name == MAGIC_GENERATOR_NAME)
            {
                continue;
            }

            LiveActor liveActor = child.GetComponent<LiveActor>();
            if (liveActor)
            {
                isUndead = (liveActor.fraction == FractionValue.Fraction.Undead);
                if(isUndead)
                {
                    return true;
                }
            }
        }
        return isUndead;
    }

    #region Player
    public bool PlayerIsLive()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.value > 0;
        }
        return false;
    }

    public uint GetPlayerHealth()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.value;
        }
        throw new Exception("Player not create");
    }

    public uint GetPlayerMaxHealth()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.maxValue;
        }
        throw new Exception("Player not create");
    }

    public uint GetPlayerBulletCount(PlayerManager.PlayerWeapon weaponType)
    {
        if (playerManager.player)
        {
            return playerManager.weaponStorage[WEAPON_NAMES[weaponType]].bulletCounter.value;
            throw new Exception("No \"" + weaponType + "\" weapon");
        }
        throw new Exception("Player not create");
    }

    #endregion



    #region Pause

    public void Pause()
    {
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i);
            SetActiveState(child, false);
        }
    }

    public void Play()
    {
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i);
            SetActiveState(child, true);
        }
    }

    private void SetActiveState(Transform child, bool isActive)
    {
        LiveActor liveActor = child.GetComponent<LiveActor>();
        Bullet bullet = child.GetComponent<Bullet>();
        InanimateActor inanimateActor = child.GetComponent<InanimateActor>();

        if (liveActor)
        {
            liveActor.isActive = isActive;
        }
        else if (bullet)
        {
            bullet.isActive = isActive;
        }
        else if (inanimateActor)
        {
            inanimateActor.isActive = isActive;
        }
    }
    #endregion


    private void CheckHealth()
    {
        Transform nodeWithActors = scene.transform;
        for (int i = 0; i < nodeWithActors.childCount; i++)
        {
            var child = nodeWithActors.GetChild(i);
            CheckHealthActor(ref child);
        }
    }

    private void CheckHealthActor(ref Transform transform)
    {
        LiveActor liveActor = transform.GetComponent<LiveActor>();
        Bullet bullet = transform.GetComponent<Bullet>();
        InanimateActor inanimateActor = transform.GetComponent<InanimateActor>();

        bool destroy = false;
        if (liveActor)
        {
            destroy = CheckHealthLiveActor(ref liveActor);
        }
        else if (bullet)
        {
            destroy = CheckHealthBullet(ref bullet);
        }
        else if (inanimateActor)
        {
            destroy = CheckHealthInanimateActor(ref inanimateActor);
        }

        if (destroy && (transform.name != MAGIC_GENERATOR_NAME))
        {
            Destroy(transform.gameObject);
        }
    }

    private static bool CheckHealthInanimateActor(ref InanimateActor inanimateActor)
    {
        return inanimateActor.health.value == 0;
    }

    private static bool CheckHealthBullet(ref Bullet bullet)
    {
        return false;
    }

    // true - уничтожить, false - нет 
    private static bool CheckHealthLiveActor(ref LiveActor liveActor)
    {
        FractionValue.Fraction fraction = liveActor.fraction;

        if (fraction != FractionValue.Fraction.Player)
        {
            if (liveActor.health.value == 0)
            {
                return true;
            }
        }
        //if((fraction.value == FractionValue.Fraction.Undead) && (liveActor.))
        //{

        //}
        return false;
    }
}
