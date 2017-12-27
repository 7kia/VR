using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code
{
    public class InanimateActor : Actor
    {
        public InanimateActor()
        {
            type.value = TypeEntity.Type.Inanimate;
        }
    }
}
