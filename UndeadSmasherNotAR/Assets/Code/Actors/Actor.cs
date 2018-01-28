using Assets.Code;
using Assets.Code.Component;
using Assets.Code.Fractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Actors
{
    public class Actor : MonoBehaviour
    {

        public EffectManager effectManager;
        public FractionValue.Fraction fraction = FractionValue.Fraction.Neutral;
        public TypeEntity type;
        public Rigidbody physicBody;

        public DynamicСharacteristic<int> health = new DynamicСharacteristic<int>();
        public bool isActive;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider col)
        {
            effectManager.handleCollision(gameObject, col.gameObject);
        }

        void OnTriggerStay(Collider col)
        {
            
        }

        void OnTriggerExit(Collider col)
        {
        }

    }
}