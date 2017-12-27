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
        // Use this for initialization
        void Start()
        {

            mapLoader.LoadMap("Level");
            gameObjectConfigManager.LoadConfig("GameObjects");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}