using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Component
{
    public class BulletCounter : DynamicСharacteristic<uint>
    {
        public bool isCountless = false;
        public new uint value
        {
            set
            {
                if(isCountless)
                {
                    currentValue = 1;
                }
                else
                {
                    base.value = value;
                }
            }
            get
            {
                return base.value;
            }
        }
    }
}
