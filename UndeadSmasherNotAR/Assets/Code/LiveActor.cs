using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveActor : Actor {

    public Weapon weapon;
    public IBehavior behavior;

    public LiveActor()
    {
        type.value = TypeEntity.Type.Live;
    }
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (behavior)
        {
            behavior.Execute(Time.deltaTime);
        }
        
	}
}
