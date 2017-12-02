using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {

        public MapLoader m_mapLoader;
        // Use this for initialization
        void Start()
        {
            m_mapLoader.LoadMap("Maps\\Levels.xml");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}