using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public ActorManager actorManager;
    public GameState gameState = GameStateManager.GameState.NotLoad;
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

	}

    public GameState GetGameState()
    {
        if (!actorManager.MagicGeneratorIsLive())
        {
            return GameState.Victory;
        }
        else if (!actorManager.PlayerIsLive())
        {
            return GameState.Defeat;
        } 
        return gameState;
        
    }
}
