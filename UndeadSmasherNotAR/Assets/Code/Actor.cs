using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    public EffectManager effectManager;
    public FractionValue fraction;
    public TypeEntity type;
    public Rigidbody physicBody;

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
