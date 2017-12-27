using Assets.Code.Behavior.BulletBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Behavior
{
    public class BehaviorFactory
    {
        // TODO : нужно передать объект для связи с внешним миром
        public static IBehavior Create(string behaviorName)
        {
            switch(behaviorName)
            {
                case "DirectFlyingBehavior":
                    return new DirectFlyingBehavior();
                case "HomingBehavior":
                    return new HomingBehavior();
                case "AggressiveBehavior":
                    return new AggressiveBehavior();
                case "ControlledBehavior":
                    return new ControlledBehavior();
            }
            return null;
        }
    }
}
