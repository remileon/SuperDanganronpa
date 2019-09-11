using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttachWeapons : MonoBehaviour {

    private Transform trans;
    public WeaponSlot[] weaponSlots;
    public float delay = 0;

    void Awake()
    {
        trans = GetComponent<Transform>();

    }
    
    // Use this for initialization
    void Start () {
        Invoke("DoAttach", Mathf.Max(delay, 0));
    }
    

    void DoAttach() {
        foreach (WeaponSlot weaponSlot in weaponSlots) {
            GameObject newWeapon = Instantiate(weaponSlot.weapon, trans.position + weaponSlot.position, trans.rotation) as GameObject;
            newWeapon.transform.Rotate(weaponSlot.rotate);
            newWeapon.transform.parent = this.transform;
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (weaponSlot.shootInterval?.Length > 0)
            {
                dict.Add("shootInterval", float.Parse(weaponSlot.shootInterval));
            }
            if (weaponSlot.rotateSpeed.Length > 0)
            {
                dict.Add("rotateSpeed", float.Parse(weaponSlot.rotateSpeed));
            }
            if (weaponSlot.bullet != null)
            {
                dict.Add("bullet", weaponSlot.bullet);
            }

            if (weaponSlot.delay?.Length > 0)
            {
                dict.Add("delay", float.Parse(weaponSlot.delay));
            }

            if (weaponSlot.bulletSpeed?.Length > 0)
            {
                dict.Add("bulletSpeed", float.Parse(weaponSlot.bulletSpeed));
            }
            if (dict.Count > 0)
            {
                newWeapon.SendMessage("ModifyAttributes", dict);
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
