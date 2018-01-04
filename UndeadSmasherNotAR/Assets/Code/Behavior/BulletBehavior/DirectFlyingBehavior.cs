using Assets.Code.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Behavior.BulletBehavior
{
    
    class DirectFlyingBehavior : IBehavior
    {
        public ActorManager actorManager;
        public Vector3 direction;

        
        public override void Execute(float deltaTime, GameObject actor)
        {
            float velocity = actor.GetComponent<Bullet>().bulletOptions.velocity;
            Vector3 shift = deltaTime * velocity * direction;

            actor.transform.position += shift;
            // TODO : движение пули
        }
    }
}
