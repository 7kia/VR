using Assets.Code.Behavior.BulletBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Behavior
{
    public class BehaviorFactory
    {
        public EffectManager effectManager;

        private DirectFlyingBehavior directFlyingBehavior = new DirectFlyingBehavior();
        private HomingBehavior homingBehavior = new HomingBehavior();
        private AggressiveBehavior aggressiveBehavior = new AggressiveBehavior();
        private ControlledBehavior controlledBehavior = new ControlledBehavior();

        public BehaviorFactory(EffectManager manager)
        {
            effectManager = manager;

            directFlyingBehavior.effectManager = manager;
            homingBehavior.effectManager = manager;
            aggressiveBehavior.effectManager = manager;
            controlledBehavior.effectManager = manager;
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
            return null;
        }
    }
}
