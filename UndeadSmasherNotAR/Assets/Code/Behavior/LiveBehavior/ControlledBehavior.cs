using Assets.Code.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Behavior.BulletBehavior
{
    

    class ControlledBehavior : IBehavior
    {
        public ActorManager actorManager;
        public override void Execute(float deltaTime, GameObject actor)
        {
            LiveActor liveActor = actor.GetComponent<LiveActor>();
            liveActor.Idle();
            //Debug.Log("ControlledBehavior Execute");
        }
    }
}
