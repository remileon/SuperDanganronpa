using System;
using UnityEngine;

public class DespairWeapon : BaseWeapon
{
    private Transform trans;

    public new void Awake()
    {
        base.Awake();
        trans = GetComponent<Transform>();
    }

    public override bool Shoot()
    {
        if (InputController.Instance.shootAction.ReadValue<float>() < 0.5f)
        {
            return false;
        }
        
        var cnt = 9;
        var positions = new[] {
            0.3f, 0.3f, 0.3f, 0.3f, 0f, -0.3f, -0.3f, -0.3f, -0.3f
        };
        var rotates = new[]
        {
            -36f, -24f, -12f, 0f, 0f, 0f, 12f, 24f, 36f
        };
        for (var i = 0; i < cnt; ++i)
        {
            var newBullet = Instantiate(bullet, trans.position, trans.rotation);
            newBullet.transform.Rotate(new Vector3(0, 0, rotates[i]));
            var rotation = trans.rotation.eulerAngles;
            var x = Math.Cos(rotation.z / 180 * Math.PI) * positions[i];
            var y = Math.Sin(rotation.z / 180 * Math.PI) * positions[i];
            if (i == 3)
            {
                Debug.Log(rotation);
            }
            newBullet.transform.position += new Vector3((float) x, (float) y, 0);
            BaseModify(newBullet);
        }

        return true;
    }
}