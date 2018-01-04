using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

using ActorNameAndParameters = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>;
namespace Assets.Code
{
    public class GameObjectConfigManager : MonoBehaviour
    {
        public UndeadSmasherObjectFactory objectFactory;

        public void LoadConfig(string mapPath)
        {
            TextAsset textAsset = Resources.Load(mapPath) as TextAsset;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("configData");
            LoadBullet(xmlNode);
            LoadWeapon(xmlNode);// TODO : возможно некорректное считывание float чисел
            LoadLiveActor(xmlNode);
            LoadInanimateActor(xmlNode);

        }

        #region ExtractData
        private ActorNameAndParameters ExtractParameters(
            XmlNode xmlDoc, 
            string groupName,
            string elementName,
            ref List<string> mainParameters,
            ref Dictionary<string, List<string>> subNodes
        )
        {
            XmlNode category = xmlDoc.SelectSingleNode(groupName);
            var categoryList = category.SelectNodes(elementName);

            ActorNameAndParameters actorNameAndParameters = new ActorNameAndParameters();
            for (int i = 0; i < categoryList.Count; i++)
            {
                var currentType = categoryList[i];
                string name = GetAtribute(ref currentType, "name");

                Dictionary<string, string> parameters = GetParameters(
                    ref currentType,
                    ref mainParameters,
                    ref subNodes
                );

                actorNameAndParameters.Add(name, parameters);
            }

            return actorNameAndParameters;
        }

        private Dictionary<string, string> GetParameters(
            ref XmlNode xmlNode,
            ref List<string> mainParameters,
            ref Dictionary<string, List<string>> subNodes
        )
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var parametr in mainParameters)
            {
                parameters.Add(parametr, GetAtribute(ref xmlNode, parametr));
            }

            foreach (var node in subNodes)
            {
                var subNode = xmlNode.SelectSingleNode(node.Key);
                foreach (var parametr in node.Value)
                {
                    parameters.Add(parametr, GetAtribute(ref subNode, parametr));
                }
            }

            return parameters;
        }
        #endregion

        #region LoadWeapon
        private void LoadWeapon(XmlNode xmlDoc)
        {
            string groupName = "weapons";
            string elementName = "weapon";

            Dictionary<string, List<string>> subNodes = new Dictionary<string, List<string>>();
            List<string> mainParameters = new List<string>();
            mainParameters.Add("name");
            mainParameters.Add("cooldown");

            List<string> bulletCounterParameters = new List<string>();
            bulletCounterParameters.Add("bulletCounter__count");
            bulletCounterParameters.Add("bulletCounter__isCountless");
            subNodes.Add("bulletCounter", bulletCounterParameters);

            var actorNameAndParameters = ExtractParameters(
                xmlDoc,
                groupName,
                elementName,
                ref mainParameters,
                ref subNodes
            );

            Debug.Log("LoadWeapon successful");
            objectFactory.actorParameters.Add(elementName, actorNameAndParameters);
            AddTypeToCategoryPairs(elementName);
            
        }

        private void AddTypeToCategoryPairs(string elementName)
        {
            foreach(var pair in objectFactory.actorParameters[elementName])
            {
                if (!objectFactory.typeToCategory.ContainsKey(elementName))
                {
                    objectFactory.typeToCategory.Add(pair.Key, elementName);
                    Debug.Log("typeToCategory = " + pair.Key + " => " + elementName);
                }
            }
        }
        #endregion

        #region LoadBullet
        private void LoadBullet(XmlNode xmlDoc)
        {
            string groupName = "bullets";
            string elementName = "bullet";

            Dictionary<string, List<string>> subNodes = new Dictionary<string, List<string>>();
            List<string> mainParameters = new List<string>();
            mainParameters.Add("name");
            mainParameters.Add("lifeTime");
            mainParameters.Add("velocity");
            mainParameters.Add("behavior");

            List<string> portionDamageParameters = new List<string>();
            portionDamageParameters.Add("portionDamage__damage");
            subNodes.Add("portionDamage", portionDamageParameters);

            var actorNameAndParameters = ExtractParameters(
                xmlDoc,
                groupName,
                elementName,
                ref mainParameters,
                ref subNodes
            );

            Debug.Log("LoadBullet successful");
            objectFactory.actorParameters.Add(elementName, actorNameAndParameters);
            AddTypeToCategoryPairs(elementName);
        }
        #endregion

        #region LoadLiveActor
        private void LoadLiveActor(XmlNode xmlDoc)
        {
            string groupName = "liveActors";
            string elementName = "liveActor";

            Dictionary<string, List<string>> subNodes = new Dictionary<string, List<string>>();
            List<string> mainParameters = new List<string>();
            mainParameters.Add("name");
            mainParameters.Add("health");
            mainParameters.Add("model");

            mainParameters.Add("fraction");
            mainParameters.Add("weapon");
            mainParameters.Add("behavior");

            List<string> colliderFeatureParameters = new List<string>();
            colliderFeatureParameters.Add("centerX");
            colliderFeatureParameters.Add("centerY");
            colliderFeatureParameters.Add("centerZ");
            colliderFeatureParameters.Add("sizeX");
            colliderFeatureParameters.Add("sizeY");
            colliderFeatureParameters.Add("sizeZ");
            subNodes.Add("colliderFeatures", colliderFeatureParameters);

            var actorNameAndParameters = ExtractParameters(
                xmlDoc,
                groupName,
                elementName,
                ref mainParameters,
                ref subNodes
            );

            Debug.Log("LoadLiveActor successful");
            objectFactory.actorParameters.Add(elementName, actorNameAndParameters);
            AddTypeToCategoryPairs(elementName);
        }


        #endregion

        #region LoadInanimateActor
        private void LoadInanimateActor(XmlNode xmlDoc)
        {
            string groupName = "inanimateActors";
            string elementName = "inanimateActor";

            Dictionary<string, List<string>> subNodes = new Dictionary<string, List<string>>();
            List<string> mainParameters = new List<string>();
            mainParameters.Add("name");
            mainParameters.Add("model");
            mainParameters.Add("health");
            mainParameters.Add("fraction");

            List<string> colliderFeatureParameters = new List<string>();
            colliderFeatureParameters.Add("centerX");
            colliderFeatureParameters.Add("centerY");
            colliderFeatureParameters.Add("centerZ");
            colliderFeatureParameters.Add("sizeX");
            colliderFeatureParameters.Add("sizeY");
            colliderFeatureParameters.Add("sizeZ");
            subNodes.Add("colliderFeatures", colliderFeatureParameters);

            var actorNameAndParameters = ExtractParameters(
                xmlDoc,
                groupName,
                elementName,
                ref mainParameters,
                ref subNodes
            );

            Debug.Log("LoadInanimateActor successful");
            objectFactory.actorParameters.Add(elementName, actorNameAndParameters);
            AddTypeToCategoryPairs(elementName);
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

