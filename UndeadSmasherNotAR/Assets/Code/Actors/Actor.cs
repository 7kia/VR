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
        public FractionValue fraction = new FractionValue();
        public TypeEntity type = new TypeEntity();
        public Rigidbody physicBody;

        public DynamicСharacteristic<uint> health = new DynamicСharacteristic<uint>();
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