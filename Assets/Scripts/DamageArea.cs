using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : Projectile
{
    public void Initialize(float lifeTime = 0.1f)
    {
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
