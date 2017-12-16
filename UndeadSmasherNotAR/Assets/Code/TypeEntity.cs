using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeEntity : MonoBehaviour {

    public enum Type
    {
        Live,
        Inanimate,
        Bullet
    }

    public Type value = Type.Inanimate;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
