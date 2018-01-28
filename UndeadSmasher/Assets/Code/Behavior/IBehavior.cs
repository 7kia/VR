using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public abstract class IBehavior
    {
        public abstract void Execute(float deltaTime, GameObject actor);
    }
}
