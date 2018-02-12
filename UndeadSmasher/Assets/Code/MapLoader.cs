using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Code
{
    public class MapLoader : MonoBehaviour
    {
        public UndeadSmasherObjectFactory objectFactory;

        /*
         * \x0 - plank block
         * \x1 - stone-brick block
         * # - air 
         */

        private Dictionary<String, String> signToNameBlock = new Dictionary<string, string>();
        private Vector3 mapSize;
        public MapLoader()
        {
            signToNameBlock.Add("p", "PlankBlock");
            signToNameBlock.Add("s", "StoneBrickBlock");
            signToNameBlock.Add("g", "IronGratingBlock");
        }

        public void LoadMap(string mapPath)
        {
            TextAsset textAsset = Resources.Load(mapPath) as TextAsset;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            LoadBlockMap(xmlDoc); 
            LoadObjectMap(xmlDoc);
 
        }

        #region LoadBlockMap
        private void LoadBlockMap(XmlDocument xmlDoc)
        {
            XmlNode blockMap = xmlDoc.SelectSingleNode("levelData").SelectSingleNode("blockMap");
            var levelList = blockMap.SelectNodes("level");

            // TODO : handle exceptions
            mapSize = new Vector3(
                float.Parse(GetAtribute(ref blockMap, "length")),
                float.Parse(GetAtribute(ref blockMap, "height")),
                float.Parse(GetAtribute(ref blockMap, "width"))
            );

            for(int i = 0; i < mapSize.y; i++)
            {
                CreateLevel(levelList[i], i);
            }
        }

        private void CreateLevel(XmlNode xmlNode, int high)
        {
            var rowList = xmlNode.SelectNodes("row");

            for (int i = 0; i < rowList.Count; i++)
            {
                CreateRow(rowList[i], i, high);
            }
        }

        private void CreateRow(XmlNode xmlNode, int width, int high)
        {
            String blocks = GetAtribute(ref xmlNode, "value");

            for(int i = 0; i < blocks.Length; i++)
            {
                if(signToNameBlock.ContainsKey(blocks[i].ToString()))
                {
                    
                    objectFactory.CreateBlock(
                        new Vector3(i - (mapSize.z / 2.0f), high, width - (mapSize.x / 2.0f)),
                        signToNameBlock[blocks[i].ToString()]
                    );
                    //Debug.Log("Create block \"" + signToNameBlock[blocks[i].ToString()] + "\"");
                }
               
            }

        }
        #endregion

        #region LoadObjectMap
        private void LoadObjectMap(XmlDocument xmlDoc)
        {
            XmlNode objectMap = xmlDoc.SelectSingleNode("levelData");
            var objectList = objectMap.SelectSingleNode("objects").SelectNodes("object");

            for (int i = 0; i < objectList.Count; i++)
            {
                var node = objectList.Item(i);
                CreateObject(ref node);
            }
        }

        private void CreateObject(ref XmlNode xmlNode)
        {
            String name = GetAtribute(ref xmlNode, "name");
            Vector3 position = new Vector3(
                float.Parse(GetAtribute(ref xmlNode, "x")),
                float.Parse(GetAtribute(ref xmlNode, "y")),
                float.Parse(GetAtribute(ref xmlNode, "z"))
            );

            objectFactory.CreateObject(position, new Quaternion(), name);
        }
        #endregion


        #region GetAtribute
        private string GetAtribute(
            ref XmlNode currNode,
            string atributeName
        )
        {
            return currNode.Attributes[atributeName].Value;
        }
        #endregion
    }
}
