using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector2 direction;//진행 방향
    private float speed;
    private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, 500f);
    }
}