using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector2 direction;//���� ����
    private float speed;
    private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, 500f);
    }
}