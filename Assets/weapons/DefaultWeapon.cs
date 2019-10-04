using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DefaultWeapon : BaseWeapon
{
    private Transform trans;
    private Bw bw;

    new public void Awake()
    {
        base.Awake();
        trans = GetComponent<Transform>();
    }

    new public void Start()
    {
        base.Start();
        bw = gameObject.GetComponentInParent<Bw>();
    }

    public override bool Shoot()
    {
        if (bw != null && InputController.Instance.shootAction.ReadValue<float>() < 0.5f)
        {
            return false;
        }

        GameObject newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation) as GameObject;
        BaseModify(newBullet);
        return true;
    }
}
