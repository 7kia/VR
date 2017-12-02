using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSmasherObjectFactory : MonoBehaviour {

    public GameObject spawnLocations;
    public Vector3 scaleFactorForBlocks;


    [SerializeField]
    public GameObject[] objectList;
    [SerializeField]
    public GameObject[] blockList;

    private Dictionary<string, GameObject> objectMap = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> blockMap = new Dictionary<string, GameObject>();
    private bool m_createNameMap = false;

    public void CreateNameMap()
    {
        for(int i = 0; i < objectList.Length; i++)
        {
            if (!objectMap.ContainsKey(objectList[i].name))
            {
                objectMap.Add(objectList[i].name, objectList[i]);
            }
        }

        for (int i = 0; i < blockList.Length; i++)
        {
            if (!blockMap.ContainsKey(blockList[i].name))
            {
                blockMap.Add(blockList[i].name, blockList[i]);
            }
        }
    }

    public void CreateObject(Vector3 position, String nameObject)
    {

        if(!m_createNameMap)
        {
            m_createNameMap = false;
            CreateNameMap();
        }

        GameObject newObject = Instantiate(
            objectMap[nameObject],
            spawnLocations.transform.position,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;

        newObject.transform.parent = spawnLocations.transform;
        newObject.transform.position = position;


        Debug.Log("GameObject Name = " + name + " position={" + newObject.transform.position.x + ", " + newObject.transform.position.y + ", " + newObject.transform.position.z + "}\n");

    }

    public void CreateBlock(Vector3 position, String nameBlock)
    {
        if (!m_createNameMap)
        {
            m_createNameMap = false;
            CreateNameMap();
        }

        GameObject newBlock = Instantiate(
            blockMap[nameBlock],
            spawnLocations.transform.position,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;

        newBlock.transform.parent = spawnLocations.transform;

        Vector3 absolutePosition = new Vector3(
            spawnLocations.transform.position.x + (position.x * scaleFactorForBlocks.x),
            spawnLocations.transform.position.y + (position.y * scaleFactorForBlocks.y),
            spawnLocations.transform.position.z + (position.z * scaleFactorForBlocks.z)
        );
        newBlock.transform.position = absolutePosition;
    }

}
