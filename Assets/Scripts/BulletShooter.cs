using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletGO;

    [SerializeField]
    private int bulletNum = 1;
    public int BulletNum
    {
        get
        {
            return bulletNum;
        }
        set
        {
            bulletNum = Mathf.Max(1, value);
        }
    }

    public BulletShootType shootType;

    public float bulletSpeedMin = 1;
    public float bulletSpeedMax = 1;
    public float bulletAngleMin = 90;
    public float bulletAngleMax = 90;
    public float bulletDamageMin = 1;
    public float bulletDamageMax = 1;

    public bool isHitOnce;//�� ���� �浹 �����Ѱ�?
    public bool isDestroyAfterHit;//�浹 �� ������°�?

    public float lifeTime = 0;
    public float lifeDistance = 0;

    public bool triger = false;

    private void Update()
    {
        if (triger)
        {
            triger = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        switch (shootType)
        {
            case BulletShootType.oneWay:
                for (int i = 0; i < bulletNum; i++)
                {
                    Quaternion quat = Quaternion.Euler(0, 0, Random.Range(bulletAngleMin, bulletAngleMax));
                    Vector2 direction = quat * Vector2.up;

                    GameObject go = Instantiate(bulletGO, transform.position, transform.rotation);
                    Bullet bullet = go.GetComponent<Bullet>();

                    bullet.Initialize(BulletMoveType.straight,
                        direction, Random.Range(bulletDamageMin, bulletDamageMax), Random.Range(bulletSpeedMin, bulletSpeedMax),
                        lifeTime: lifeTime, lifeDistance: lifeDistance);
                }
                break;

            case BulletShootType.fan:
                {
                    Quaternion quat = Quaternion.Euler(0, 0, bulletAngleMin);
                    Quaternion quatAtOnce = Quaternion.Euler(0, 0, (bulletAngleMax - bulletAngleMin) / bulletNum);

                    for (int i = 0; i < bulletNum; i++)
                    {
                        Vector2 direction = quat * Vector2.up;

                        GameObject go = Instantiate(bulletGO, transform.position, transform.rotation);
                        Bullet bullet = go.GetComponent<Bullet>();

                        bullet.Initialize(BulletMoveType.straight,
                            direction, Random.Range(bulletDamageMin, bulletDamageMax), Random.Range(bulletSpeedMin, bulletSpeedMax),
                            lifeTime: lifeTime, lifeDistance: lifeDistance);

                        quat *= quatAtOnce;
                    }
                }
                break;

            default:
                break;
        }

    }
}

public enum BulletShootType
{
    oneWay,
    fan,
}