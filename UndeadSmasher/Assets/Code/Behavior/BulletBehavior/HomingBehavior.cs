using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Behavior.BulletBehavior
{
    
    class HomingBehavior : IBehavior
    {
        public ActorManager actorManager;
        public GameObject target;
        public override void Execute(float deltaTime, GameObject actor)
        {

        }
    }
}
