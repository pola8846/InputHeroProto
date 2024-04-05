using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2D : Unit
{
    private bool canMove = true;
    [Header("이동")]
    [SerializeField]
    private float speed = 3f;
    private float movement = 0;

    [Header("대시")]
    [SerializeField]
    private float dashSpeed = 20f;
    [SerializeField]
    private float dashDistance = 4f;

    [Header("점프")]
    [SerializeField]
    private float jumpForce = 8f;

    [Header("원거리 공격")]
    [SerializeField]
    private float atkRangeDamage = 1f;
    [SerializeField]
    private float atkRangeCooltime = 0.4f;
    [SerializeField]
    private float atkRangeSpeed = 1f;

    [Header("근거리 공격")]
    [SerializeField]
    private float atkMeleeCooltime = 0.3f;
    [SerializeField]
    private float atkMeleeLifetime = 0.05f;
    private Vector3 atkSide;
    private bool atkSideB;
    private float atkTimer;

    private Rigidbody2D rb;

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
                if (canMove)
                {
                    movement = speed * -1;
                    //transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.left;
                    atkSide = Vector2.left;
                    atkSideB = false;
                }
                break;
            case Action.MoveRight:
                if (canMove)
                {
                    movement = speed;
                    //transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.right;
                    atkSide = Vector2.right;
                    atkSideB = true;
                }
                break;
            case Action.MoveStop:
                movement = 0;
                break;
            case Action.AttackRange:
                Shoot();
                break;
            case Action.AttackMelee:
                MeleeAttack();
                break;
            case Action.Jump:
                if (canMove)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jumpForce);
                }
                break;
            case Action.DashLeft:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity=new Vector2(movement, rb.velocity.y);
        //rb.MovePosition(rb.position + (Vector2)movement * Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        if (Time.time - atkTimer >= atkRangeCooltime)
        {
            shooter.bulletSpeedMax = shooter.bulletSpeedMin = atkRangeSpeed;
            shooter.bulletAngleMax = shooter.bulletAngleMin = atkSideB ? -90 : 90;
            shooter.bulletDamageMax = shooter.bulletDamageMin = atkRangeDamage;
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

    public void Dash(Vector2 dir)
    {

    }
}
