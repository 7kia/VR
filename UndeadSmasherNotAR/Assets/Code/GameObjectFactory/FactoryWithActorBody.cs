using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWithActorBody : MonoBehaviour {

    public ActorBodyManager actorBodyManager;

    protected void CreateModelForActor(
        GameObject newObject,
        Vector3 position,
        string modelName
    )
    {
        if (!actorBodyManager.modelDictionary.ContainsKey(modelName))
        {
            foreach(var key in actorBodyManager.modelDictionary.Keys)
            {
                Debug.Log(key);
            }

        }

        if(!actorBodyManager.modelDictionary.ContainsKey(modelName))
        {
            Debug.Log(modelName);
        }

        GameObject newModel = Instantiate(
               actorBodyManager.modelDictionary[modelName],
               position,
               Quaternion.Euler(0, 0, 0)
           ) as GameObject;
        newModel.transform.parent = newObject.transform;
    }

    protected void SetCollider(Dictionary<string, string> otherParameters, GameObject newObject)
    {
        BoxCollider actorCollider = newObject.GetComponent<BoxCollider>();
        actorCollider.size = new Vector3(
            float.Parse(otherParameters["sizeX"]),
            float.Parse(otherParameters["sizeY"]),
            float.Parse(otherParameters["sizeZ"])
        );
        actorCollider.center = new Vector3(
            float.Parse(otherParameters["centerX"]),
            float.Parse(otherParameters["centerY"]),
            float.Parse(otherParameters["centerZ"])
        );
    }
}
