using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    public EffectManager effectManager;
    public FractionValue fraction;
    public TypeEntity type = new TypeEntity();
    public Rigidbody physicBody;

    public DynamicСharacteristic<int> health;

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
        Debug.Log("OnTriggerEnter");
        effectManager.handleCollision(gameObject, col.gameObject);
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("OnTriggerStay");
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("OnTriggerExit");
    }

}
