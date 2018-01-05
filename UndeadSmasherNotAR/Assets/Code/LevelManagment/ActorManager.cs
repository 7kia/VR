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
    public static string PLAYER_FIRST_WEAPON_NAME = "PlayerWeapon";
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
        playerManager.weapons[0] = objectFactory.CreateObject(playerPosition, PLAYER_FIRST_WEAPON_NAME);
    }

    public bool MagicGeneratorIsLive()
    {
        return false;
    }

    public bool ContentUndead()
    {
        return false;
    }

    public bool PlayerIsLive()
    {
        return playerManager.player.GetComponent<LiveActor>().health.value > 0;
    }

    private void CheckHealth()
    {
        Transform nodeWithActors = scene.transform;
        for (int i = 0; i < nodeWithActors.childCount; i++)
        {
            CheckHealthActor(nodeWithActors.GetChild(i));
        }
    }

    private void CheckHealthActor(Transform transform)
    {
        LiveActor liveActor = transform.GetComponent<LiveActor>();
        Bullet bullet = transform.GetComponent<Bullet>();
        InanimateActor inanimateActor = transform.GetComponent<InanimateActor>();

        bool destroy = false;
        if (liveActor)
        {
            destroy = CheckHealthLiveActor(liveActor);
        }
        else if (bullet)
        {
            destroy = CheckHealthBullet(bullet);
        }
        else if (inanimateActor)
        {
            destroy = CheckHealthInanimateActor(inanimateActor);
        }

        if (destroy)
        {
            Destroy(transform.gameObject);
        }
    }

    private bool CheckHealthInanimateActor(InanimateActor inanimateActor)
    {
        return inanimateActor.health.value == 0;
    }

    private bool CheckHealthBullet(Bullet bullet)
    {
        return false;
    }

    // true - уничтожить, false - нет 
    private bool CheckHealthLiveActor(LiveActor liveActor)
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
