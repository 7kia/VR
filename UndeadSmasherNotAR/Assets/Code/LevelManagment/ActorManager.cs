using Assets.Code;
using Assets.Code.Actors;
using Assets.Code.LevelManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Code.Fractions;

public class ActorManager : MonoBehaviour {

    public EffectManager effectManager;
    public PlayerManager playerManager;
    public GameObject scene;
    public UndeadSmasherObjectFactory objectFactory;

    public bool checkActors = false;
    public static string PLAYER_ENTITY_NAME = "MagicEye";
    public static string PLAYER_COBBLE_WEAPON_NAME = "PlayerCobbleWeapon";
    public static string PLAYER_BOMB_WEAPON_NAME = "PlayerBombWeapon";
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

    public void GeneratePlayer()
    {
        Vector3 playerPosition = playerManager.playerCamera.transform.position;

        playerManager.player = objectFactory.CreateObject(playerPosition, PLAYER_ENTITY_NAME);

        // WARNING : не забудь проверить кол-во оружия
        playerManager.weapons[0] = objectFactory.CreateObject(playerPosition, PLAYER_COBBLE_WEAPON_NAME);
        playerManager.weapons[1] = objectFactory.CreateObject(playerPosition, PLAYER_BOMB_WEAPON_NAME);

        for(int i = 0; i < 2; i++)
        {
            Weapon weapon = playerManager.weapons[i].GetComponent<Weapon>();
            LiveActor liveActor = playerManager.player.GetComponent<LiveActor>();
            weapon.owner = playerManager.player;
            weapon.objectFactory = objectFactory;
            weapon.bulletOptions.fraction = liveActor.fraction;
        }
    }

    public void ClearScene()
    {
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            if (child)
            {
                Destroy(child);
            }
        }
    }

    public bool MagicGeneratorIsLive()
    {
        return true;
    }

    public bool ContentUndead()
    {
        return false;
    }

    public bool PlayerIsLive()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.value > 0;
        }
        return false;
    }

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

        if (destroy)
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
        FractionValue fraction = liveActor.fraction;

        if (fraction.value != FractionValue.Fraction.Player)
        {
            if (liveActor.health.value == 0)
            {
                return true;
            }
        }
        return false;
    }
}
