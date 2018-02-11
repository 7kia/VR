using Assets.Code;
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

    public GameObject magicGenerator;

    public static string MAGIC_GENERATOR_NAME = "MagicGenerator";
    public static string PLAYER_ENTITY_NAME = "MagicEye";

    public Dictionary<PlayerManager.PlayerWeapon, string> WEAPON_NAMES = new Dictionary<PlayerManager.PlayerWeapon, string>();

    private bool updateActors = false;

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
		if (updateActors)
        {
            UpdateActors();
            CheckActors();
        }
	}

    #region UpdateActors
    private void UpdateActors()
    {
        Transform nodeWithActors = scene.transform;
        for (int i = 0; i < nodeWithActors.childCount; i++)
        {
            var child = nodeWithActors.GetChild(i);
            UpdateActor(child);
        }
    }

    private void UpdateActor(Transform child)
    {
        LiveActor liveActor = child.GetComponent<LiveActor>();
        Bullet bullet = child.GetComponent<Bullet>();
        InanimateActor inanimateActor = child.GetComponent<InanimateActor>();

        if (liveActor)
        {
            liveActor.UpdateActor();
        }
        else if (bullet)
        {
            bullet.UpdateActor();
        }
        else if (inanimateActor)
        {
            inanimateActor.UpdateActor();
        }
    }


    #endregion

    public void FindMagicGenerator()
    {
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            if (child)
            {
                if(child.name == MAGIC_GENERATOR_NAME)
                {
                    magicGenerator = child;
                    Debug.Log("Magic generator found");
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
    #endregion
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
            if(playerManager.weapons[i])
            {
                Destroy(playerManager.weapons[i]);
            }
        }
        playerManager.ClearPlayerWeaponData();
    }

    public void CreatePlayerWeapons(PlayerManager.PlayerWeapon[] weaponNames, Vector3 playerPosition)
    {
        for (uint i = 0; i < weaponNames.Length; ++i)
        {
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
        player.weapon = playerManager.weapons[0].GetComponent<Weapon>();
        player.weapon.owner = playerManager.player;



    }

    #region ClearScene
    public void ClearScene()
    {
        magicGenerator = null;
        playerManager.ClearPlayerData();
        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            if (child)
            {
                //ClearChilds(child);
                Destroy(child);
            }
        }
        
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

    #region ForSetAward
    public bool MagicGeneratorIsLive()
    {
        return magicGenerator.GetComponent<LiveActor>().health.value > 0;
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

    public uint GetAward()
    {
        uint award = 0;
        if (!MagicGeneratorIsLive())
        {
            award++;
            if (!ContentUndead())
            {
                award++;
            }
            if (playerManager.RemainedBullet())
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
    #endregion

    #region PlayerInfo
    public bool PlayerIsLive()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.value > 0;
        }
        return false;
    }

    public int GetPlayerHealth()
    {
        if (playerManager.player)
        {
            return playerManager.player.GetComponent<LiveActor>().health.value;
        }
        throw new Exception("Player not create");
    }

    public int GetPlayerMaxHealth()
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



    #region PauseState

    public void Pause()
    {
        updateActors = false;
    }

    public void Play()
    {
        updateActors = true;
    }

    public bool IsUpdate()
    {
        return updateActors;
    }
    #endregion


    private void CheckActors()
    {
        Transform nodeWithActors = scene.transform;
        for (int i = 0; i < nodeWithActors.childCount; i++)
        {
            var child = nodeWithActors.GetChild(i);
            if(!CheckHealthActor(ref child))
            {
                
            }
            if (child != null)
            {
                CheckLifeTime(ref child);
            }
        }
    }

    #region CheckLifeTime
    private void CheckLifeTime(ref Transform child)
    {
        LiveActor liveActor = child.GetComponent<LiveActor>();
        Bullet bullet = child.GetComponent<Bullet>();
        InanimateActor inanimateActor = child.GetComponent<InanimateActor>();


        bool destroy = false;
        if (liveActor)
        {
            destroy = CheckLiveActorLifeTime(ref liveActor);
        }
        else if (bullet)
        {
            destroy = CheckBulletLifeTime(ref bullet);
        }
        else if (inanimateActor)
        {
            destroy = CheckInanimateActorLifeTime(ref inanimateActor);
        }

        if (destroy)
        {
            Destroy(child.gameObject);
        }
    }

    private bool CheckLiveActorLifeTime(ref LiveActor liveActor)
    {
        return false;
    }

    private bool CheckBulletLifeTime(ref Bullet bullet)
    {
        return bullet.lifeTimer.NowTimeMoreMax();
    }

    private bool CheckInanimateActorLifeTime(ref InanimateActor inanimateActor)
    {
        return false;
    }

    #endregion

    #region CheckHealth
    private bool CheckHealthActor(ref Transform child)
    {
        LiveActor liveActor = child.GetComponent<LiveActor>();
        Bullet bullet = child.GetComponent<Bullet>();
        InanimateActor inanimateActor = child.GetComponent<InanimateActor>();

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

        destroy = (destroy && (child.name != MAGIC_GENERATOR_NAME));
        if (destroy)
        {
            Destroy(child.gameObject);
        }

        return destroy;
    }

    private static bool CheckHealthInanimateActor(ref InanimateActor inanimateActor)
    {
        return inanimateActor.health.value <= 0;
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
            if (liveActor.health.value <= 0)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
   
}
