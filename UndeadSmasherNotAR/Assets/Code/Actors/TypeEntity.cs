using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeEntity {

    public enum Type
    {
        Live,
        Inanimate,
        Bullet
    }

    public Type value = Type.Inanimate;
}
