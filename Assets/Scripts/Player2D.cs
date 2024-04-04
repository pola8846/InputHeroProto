using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Unit
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float jumpForce = 8f;
    [SerializeField]
    private float atkRangeDamage = 1f;
    [SerializeField]
    private float atkRangeCooltime = 0.4f;
    [SerializeField]
    private float atkRangeSpeed = 1f;
    [SerializeField]
    private float atkMeleeCooltime = 0.3f;
    [SerializeField]
    private float atkMeleeLifetime = 0.05f;
    private Vector3 atkSide;
    private bool atkSideB;
    private float atkTimer;

    private Rigidbody2D rb;

    private Vector3 lastVelocity;
    [SerializeField]
    private GameObject dmgArea;
    [SerializeField]
    private GameObject AtkPosL;
    [SerializeField]
    private GameObject AtkPosR;

    private BulletShooter shooter;
    private void Start()
    {
        GameManager.SetPlayer(this);
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<BulletShooter>();
    }

    public void DoAction(Action actionCode)
    {
        switch (actionCode)
        {
            case Action.MoveUp:
                break;
            case Action.MoveDown:
                break;
            case Action.MoveLeft:
                transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.left;
                atkSide = Vector2.left;
                atkSideB = false;
                break;
            case Action.MoveRight:
                transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.right;
                atkSide = Vector2.right;
                atkSideB = true;
                break;
            case Action.MoveStop:
                break;
            case Action.AttackRange:
                Shoot();
                break;
            case Action.AttackMelee:
                MeleeAttack();
                break;
            case Action.Jump:
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
                break;
            default:
                break;
        }
    }

    public void Shoot()
    {
        if (Time.time - atkTimer >= atkRangeCooltime)
        {
            shooter.bulletSpeedMax = shooter.bulletSpeedMin = atkRangeSpeed;
            shooter.bulletAngleMax = shooter.bulletAngleMin = atkSideB ? -90 : 90;
            shooter.triger = true;
            atkTimer = Time.time;
        }
    }

    public void MeleeAttack()
    {
        if (Time.time - atkTimer >= atkMeleeCooltime)
        {
            Instantiate(dmgArea, atkSideB ? AtkPosR.transform.position : AtkPosL.transform.position, Quaternion.identity).GetComponent<DamageArea>().Initialize(atkMeleeLifetime);
            atkTimer = Time.time;
        }

    }
}
