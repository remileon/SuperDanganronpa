using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWeapon : BaseWeapon
{
//    [SerializeField] private float range = (float) (Math.PI / 2);
    [SerializeField] private float range = (float) (Math.PI / 2);
    private Transform trans;

    public new void Awake()
    {
        base.Awake();
        trans = GetComponent<Transform>();
    }

    public override void Shoot()
    {
        var newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation);
        var angle = Random.Range(-range / 2, range / 2);
        newBullet.transform.Rotate(0, 0, angle);
        BaseModify(newBullet);
    }
}