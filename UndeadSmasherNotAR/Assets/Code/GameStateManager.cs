using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public bool nowPause = false;
    public ActorManager actorManager;

    public enum GameState
    {
        Play,
        Pause,
        Defeat,
        Victory
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (nowPause)
        {

        }
	}

    public GameState GetGameState()
    {
        if (nowPause)
        {
            if (!actorManager.MagicGeneratorIsLive())
            {
                //if (actorManager.ContentUndead())
                //{
                //    return GameState.Victory
                //    // Give extra points 
                //}
                return GameState.Victory;
            }
            else if (!actorManager.PlayerIsLive())
            {
                return GameState.Defeat;
            }
            return GameState.Play;
        }
        return GameState.Pause;
    }
}
