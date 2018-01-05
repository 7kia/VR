using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {

        public MapLoader mapLoader;
        public GameObjectConfigManager gameObjectConfigManager;
        public ActorManager actorManager;
        // Use this for initialization
        void Start()
        {
            gameObjectConfigManager.LoadConfig("GameObjects");
            mapLoader.LoadMap("Level");
            actorManager.GeneratePlayer();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}