using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Component
{
    public class DynamicСharacteristic<T>
    {
        static private bool LeftMoreRight(T lhs, T rhs)
        {
            return Comparer<T>.Default.Compare(lhs, rhs) > 0;
        }

        protected T currentValue;
        public T maxValue;
        
        public T value
        {
            set
            {
                if (LeftMoreRight(value, maxValue))
                {
                    currentValue = maxValue;
                }
                else
                {
                    currentValue = value;
                }
            }
            get { return currentValue; }
        }
        

    }
}
