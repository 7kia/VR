using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public ActorManager actorManager;

    public enum GameState
    {
        NotLoad,
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
        if (actorManager.IsUpdate())
        {

        }
	}

    public GameState GetGameState()
    {
        if (actorManager.IsUpdate())
        {
            if (!actorManager.MagicGeneratorIsLive())
            {
                Debug.Log("!MagicGeneratorIsLive");
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
