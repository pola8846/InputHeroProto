using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector2 direction;//진행 방향
    private float turnSpeed;
    private float speed;
    private BulletMoveType moveType;

    public void Initialize(BulletMoveType moveType, Vector2 dir, float speed, float turnSpeed=0, float lifeTime = 0)
    {
        this.moveType = moveType;
        direction = dir;
        this.speed = speed;
        this.turnSpeed = turnSpeed;
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    private void Update()
    {
        if (direction==null)
        {
            return;
        }
        switch (moveType)
        {
            case BulletMoveType.straight:
                transform.Translate(direction * speed * GameManager.Instance.gameSpeed * GameManager.Instance.bulletSpeed);
                break;
            case BulletMoveType.curve:
                
                transform.Translate(direction * speed * GameManager.Instance.gameSpeed * GameManager.Instance.bulletSpeed);
                break;
            default:
                break;
        }
    }
}

public enum BulletMoveType
{
    straight,
    curve,
}