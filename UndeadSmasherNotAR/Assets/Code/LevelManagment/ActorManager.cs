using Assets.Code;
using Assets.Code.Actors;
using Assets.Code.LevelManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour {

    public EffectManager effectManager;
    public PlayerManager playerManager;
    public GameObject scene;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool magicGeneratorIsLive()
    {
        return false;
    }

    public bool contentUndead()
    {
        return false;
    }

    public bool playerIsLive()
    {
        return playerManager.player.GetComponent<LiveActor>().health.value > 0;
    }
}
