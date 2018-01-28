using Assets.Code.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Actors
{
    public enum TypeEntity
    {
        Live,
        Inanimate,
        Bullet
    }

    public class TypeEntityFunctions
    {
        public static TypeEntity GetType(GameObject gameObject)
        {
            LiveActor liveActor = gameObject.transform.GetComponent<LiveActor>();
            Bullet bullet = gameObject.transform.GetComponent<Bullet>();
            InanimateActor inanimateActor = gameObject.transform.GetComponent<InanimateActor>();

            if (liveActor)
            {
                return liveActor.type;
            }
            else if (bullet)
            {
                return bullet.type;
            }
            else if (inanimateActor)
            {
                return inanimateActor.type;
            }

            return TypeEntity.Inanimate;
        }
    }

    
}
