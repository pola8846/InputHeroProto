using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : Projectile
{

    private Vector2 direction;//진행 방향
    private float turnSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float alterSpd;
    private BulletMoveType1 moveType;
    private float lifeDistance = 0f;

    private Vector3 originPos;

    private void Start()
    {
        originPos = transform.position;

    }

    public void Initialize(BulletMoveType1 moveType, Vector2 dir, float damage, float speed, float turnSpeed = 0, float lifeTime = 0f, float lifeDistance = 0f)
    {
        this.moveType = moveType;
        direction = dir;
        
        this.alterSpd = speed;
        this.turnSpeed = turnSpeed;
        this.damage = damage;
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        } 
        this.lifeDistance = lifeDistance;
        originPos = transform.position;
    }

    private void Update()
    {
     
        if (lifeDistance > 0 && Vector3.Distance(originPos, transform.position) >= lifeDistance)
        {
            Destroy(gameObject);
            return;
        }
        if (direction == null)
        {
            return;
        }
        switch (moveType)
        {
            case BulletMoveType1.straight:
                speed = Mathf.Lerp(alterSpd, 0.5f, 0.1f * Time.deltaTime);
                transform.Translate(GameManager.Instance.bulletSpeed * GameManager.Instance.gameSpeed * speed * Time.deltaTime * direction);
                break;
            case BulletMoveType1.curve:
                transform.Translate(GameManager.Instance.bulletSpeed * GameManager.Instance.gameSpeed * speed * Time.deltaTime * direction);
                Quaternion quat = Quaternion.Euler(0, 0, turnSpeed * GameManager.Instance.gameSpeed * GameManager.Instance.bulletSpeed * Time.deltaTime);
                direction = quat * direction;
                break;
            default:
                break;
        }
    }
}

public enum BulletMoveType1
{
    straight,
    curve,

}

