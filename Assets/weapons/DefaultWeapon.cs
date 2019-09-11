using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : BaseWeapon
{
    private Transform trans;

    new public void Awake()
    {
        base.Awake();
        trans = GetComponent<Transform>();
    }

    public override void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation) as GameObject;
        BaseModify(newBullet);
    }
}
