using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBodyManager : MonoBehaviour {

    public GameObject[] models;
    public Dictionary<string, GameObject> modelDictionary = new Dictionary<string, GameObject>();


    public ActorBodyManager()
    {

    }
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadModels()
    {
        if (modelDictionary.Count == 0)
        {
            for (int i = 0; i < models.Length; i++)
            {
                //Debug.Log(models[i].name);
                if (!modelDictionary.ContainsKey(models[i].name))
                {
                    modelDictionary.Add(models[i].name, models[i]);
                    Debug.Log(models[i].name);
                }
            }
        }
        
    }
}
