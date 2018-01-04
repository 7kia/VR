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
        public Vector3 target;
        public override void Execute(float deltaTime, GameObject actor)
        {

        }
    }
}
