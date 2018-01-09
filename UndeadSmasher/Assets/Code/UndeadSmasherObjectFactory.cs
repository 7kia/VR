using Assets.Code;
using Assets.Code.Behavior;
using Assets.Code.Fractions;
using Assets.Code.GameObjectFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ActorNameAndParameters = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
public class UndeadSmasherObjectFactory : MonoBehaviour {

    public GameObject spawnLocations;
    public Vector3 scaleFactorForBlocks;

    public ActorManager actorManager;
    public ActorBodyManager actorBodyManager;
    public EffectManager effectManager;
    public BehaviorFactory behaviorFactory;
    [SerializeField]
    public GameObject[] objectList;
    [SerializeField]
    public string[] blockList;

    private Dictionary<string, GameObject> objectMap = new Dictionary<string, GameObject>();

    // Ключ - название категории("Снаряд", например), Значение - виды("Стрела", "Бомба" и.т.д)
    public Dictionary<string, ActorNameAndParameters> actorParameters = new Dictionary<string, ActorNameAndParameters>();
    public Dictionary<string, string> typeToCategory = new Dictionary<string, string>();


    private bool m_createNameMap = false;// TODO : надо ли?
    private BulletFactory bulletFactory = new BulletFactory();
    private WeaponFactory weaponFactory = new WeaponFactory();

    private InanimateActorFactory inanimateActorFactory = new InanimateActorFactory();
    private LiveActorFactory liveActorFactory = new LiveActorFactory();

    public void CreateNameMap()
    {
        for(int i = 0; i < objectList.Length; i++)
        {
            if (!objectMap.ContainsKey(objectList[i].name))
            {
                objectMap.Add(objectList[i].name, objectList[i]);
            }
        }

        SetFactoryOptions();
    }

    private void SetFactoryOptions()
    {
        SetBehaviorFactory();// Важен порядок функции, эта первая
        SetWeaponFactory();
        SetBulletFactory();
        SetInanimateActorFactory();
        SetLiveActorFactory();
    }

    private void SetBehaviorFactory()
    {
        behaviorFactory = new BehaviorFactory(actorManager);
    }

    private void SetBulletFactory()
    {
        bulletFactory.prefub = objectMap["Bullet"];
        bulletFactory.effectManager = effectManager;
        bulletFactory.behaviorFactory = behaviorFactory;
        bulletFactory.actorBodyManager = actorBodyManager;
    }

    private void SetWeaponFactory()
    {
        weaponFactory.prefub = objectMap["Weapon"];
        weaponFactory.objectFactory = this;
        weaponFactory.effectManager = effectManager;
    }

    private void SetInanimateActorFactory()
    {
        inanimateActorFactory.prefub = objectMap["InanimateActor"];
        inanimateActorFactory.effectManager = effectManager;
        inanimateActorFactory.actorBodyManager = actorBodyManager;
    }

    private void SetLiveActorFactory()
    {
        liveActorFactory.prefub = objectMap["LiveActor"];
        liveActorFactory.objectFactory = this;
        liveActorFactory.effectManager = effectManager;
        liveActorFactory.behaviorFactory = behaviorFactory;
        liveActorFactory.weaponFactory = weaponFactory;
        liveActorFactory.actorBodyManager = actorBodyManager;
    }

    public GameObject CreateObject(Vector3 position, String nameObject)
    {

        if(!m_createNameMap)
        {
            m_createNameMap = false;
            CreateNameMap();
        }

        GameObject newObject = CreateUnallocatedObject(nameObject);

        newObject.transform.parent = spawnLocations.transform;
        newObject.transform.position = position;

        return newObject;
        //Debug.Log("GameObject Name = " + name + " position={" + newObject.transform.position.x + ", " + newObject.transform.position.y + ", " + newObject.transform.position.z + "}\n");

    }

    private GameObject CreateUnallocatedObject(string nameObject)
    {
        GameObject newObject = null;

        string newObjectCategory = typeToCategory[nameObject];
        Dictionary<string, string> parametres = actorParameters[newObjectCategory][nameObject];

        switch (newObjectCategory)
        {
            case "bullet":
                newObject = bulletFactory.Create(
                    spawnLocations.transform.position,
                    parametres
                );
                break;
            case "inanimateActor":
                newObject = inanimateActorFactory.Create(
                    spawnLocations.transform.position,
                    parametres
                );
                break;
            case "liveActor":
                newObject = liveActorFactory.Create(
                    spawnLocations.transform.position,
                    parametres
                );
                break;
            case "weapon":// TODO : имя?
                newObject = weaponFactory.Create(
                    spawnLocations.transform.position,
                    parametres
                );
                break;
        }
        return newObject;
    }


    public void CreateBlock(Vector3 position, String nameBlock)
    {
        if (!m_createNameMap)
        {
            m_createNameMap = false;
            CreateNameMap();
        }

        GameObject newBlock = CreateUnallocatedObject(nameBlock);


        newBlock.transform.parent = spawnLocations.transform;

        Vector3 absolutePosition = new Vector3(
            spawnLocations.transform.position.x + (position.x * scaleFactorForBlocks.x),
            spawnLocations.transform.position.y + (position.y * scaleFactorForBlocks.y),
            spawnLocations.transform.position.z + (position.z * scaleFactorForBlocks.z)
        );
        newBlock.transform.position = absolutePosition;
    }

     

}
