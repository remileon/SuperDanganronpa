using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponSlot {
    public Vector3 position;
    public Vector3 rotate;
    public GameObject weapon;

    // can't inspect a nullable value (not primitive or UnityEngine.Object)
    // parse by code, empty string means null
    public string shootInterval;
    public string rotateSpeed;
    public GameObject bullet;
}