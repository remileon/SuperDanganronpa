using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoWeapon : BaseWeapon
{
    private Transform trans;

    public void Awake()
    {
        base.Awake();
        trans = GetComponent<Transform>();
    }

    public override void Shoot()
    {
        for (int i = 0; i < 8; ++i)
        {
            GameObject newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation) as GameObject;
            newBullet.transform.Rotate(new Vector3(0, 0, 45 * i));
            BaseModify(newBullet);
        }
    }
}
