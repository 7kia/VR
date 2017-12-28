using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Component
{
    public class MyTimer
    {
        public float time = 0.0f;
        public float maxTime = 0.0f;
        public bool m_endTime = false;
        private bool m_stopTimer = true;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!m_stopTimer)
            {
                time -= Time.deltaTime;
                if (time <= 0f)
                {
                    m_endTime = true;
                    StopTimer();
                }
            }
        }

        public void ResetTimer()
        {
            time = maxTime;
            m_endTime = false;
        }

        public void StopTimer()
        {
            m_stopTimer = true;
        }

        public void PlayTimer()
        {
            m_stopTimer = false;
        }

        public void AddToTime(float deltatime)
        {
            time += deltatime;
        }

        public bool NowTimeMoreMax()
        {
            return time >= maxTime;
        }

    }
}
