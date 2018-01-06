using Assets.Code.Behavior.BulletBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Behavior
{
    public class BehaviorFactory
    {
        public ActorManager actorManager;

        private DirectFlyingBehavior directFlyingBehavior = new DirectFlyingBehavior();
        private HomingBehavior homingBehavior = new HomingBehavior();
        private AggressiveBehavior aggressiveBehavior = new AggressiveBehavior();
        private ControlledBehavior controlledBehavior = new ControlledBehavior();

        public BehaviorFactory(ActorManager manager)
        {
            actorManager = manager;

            directFlyingBehavior.actorManager = manager;
            homingBehavior.actorManager = manager;
            aggressiveBehavior.actorManager = manager;
            controlledBehavior.actorManager = manager;
        }

        // TODO : нужно передать объект для связи с внешним миром
        public IBehavior Create(string behaviorName)
        {
            switch(behaviorName)
            {
                case "DirectFlyingBehavior":
                    return directFlyingBehavior;
                case "HomingBehavior":
                    return homingBehavior;
                case "AggressiveBehavior":
                    return aggressiveBehavior;
                case "ControlledBehavior":
                    return controlledBehavior;
            }
            throw new NotImplementedException();
        }
    }
}
