using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2D : Unit
{
    private bool canMove = true;
    private bool isDashing = false;
    [SerializeField]
    private Queue<Effect> effectQueue = new();
    [SerializeField]
    private int effectQueueChecker = 0;//�ߵ� ���� ȿ�� ����. 0�̿��� �� ȿ���� ����
    [SerializeField]
    private int effectCanMoveChecker = 0;//�̵� �Ұ��� �� ȿ�� ����. 0�̿��� �̵� ����
    [Header("�̵�")]
    [SerializeField]
    private float speed = 3f;
    private float movement = 0;
    private Vector2 movement_Dash;

    [Header("���")]
    [SerializeField]
    private float dashSpeed = 20f;
    [SerializeField]
    private float dashDistance = 4f;

    [Header("����")]
    [SerializeField]
    private float jumpForce = 8f;

    [Header("���Ÿ� ����")]
    [SerializeField]
    private float atkRangeDamage = 1f;
    [SerializeField]
    private float atkRangeCooltime = 0.4f;
    [SerializeField]
    private float atkRangeSpeed = 1f;

    [Header("�ٰŸ� ����")]
    [SerializeField]
    private float atkMeleeCooltime = 0.3f;
    [SerializeField]
    private float atkMeleeLifetime = 0.05f;
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
    private Color colorOrigin;
    private void Start()
    {
        GameManager.SetPlayer(this);
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponentInChildren<BulletShooter>();
        StartCoroutine(EffectChecker());
        colorOrigin = gameObject.GetComponent<Renderer>().material.color;
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
                if (canMove && effectCanMoveChecker == 0)
                {
                    movement = Mathf.Max(0, speed) * -1;
                    //transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.left;
                    atkSideB = false;
                }
                break;
            case Action.MoveRight:
                if (canMove && effectCanMoveChecker == 0)
                {
                    movement = Mathf.Max(0, speed);
                    //transform.position += speed * Time.deltaTime * GameManager.Instance.gameSpeed * Vector3.right;
                    atkSideB = true;
                }
                break;
            case Action.MoveStop:
                if (canMove && effectCanMoveChecker == 0)
                {
                    movement = 0;
                }
                break;
            case Action.AttackRange:
                //Shoot();
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

    public void DoAction(SkillTiming timing, Skill skill, bool stopStay = false)
    {
        //Debug.Log($"{timing}, {stopStay}");
        foreach (Effect effect in skill.Effects)
        {
            switch (timing)
            {
                case SkillTiming.Down://���� ��
                case SkillTiming.Up://�� ��
                    AddEffectAtQueue(effect);
                    break;

                case SkillTiming.Stay://������ ������ ��
                    if (!stopStay)//���� ��
                    {
                        AddEffectAtQueue(effect);
                    }
                    else//�� ��
                    {
                        RemoveAll(effectQueue, effect.GetType());//ť���� ȿ�� ����
                        //ȿ�� ���� �־�� ��
                    }
                    break;

                default:
                    break;
            }
        }

    }

    private void FixedUpdate()
    {
        if (canMove && effectCanMoveChecker == 0)
        {
            rb.velocity = new Vector2(movement, rb.velocity.y);
        }
        else if (isDashing)
        {
            rb.velocity = movement_Dash;
        }
    }

    public void Attack()
    {
        if (Time.time - atkTimer >= atkRangeCooltime)
        {
            shooter.bulletSpeedMax = shooter.bulletSpeedMin = Mathf.Max(0, atkRangeSpeed);
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

    public void AddEffectAtQueue(Effect effect)//ť�� ȿ�� �߰�. 
    {
        if (effectQueueChecker == 0)
        {
            effectQueue.Enqueue(effect);
        }
    }

    public IEnumerator EffectChecker()
    {
        while (true)
        {
            if (effectQueueChecker == 0)//ȿ���� ���� ���� �ƴϸ�
            {
                while (effectQueue.Count >= 1)//���� ��� ���� ȿ���� ������ �����ڳ� ��� ������ false�� ȿ���� ���� ������ ����
                {
                    Effect effect = effectQueue.Peek();
                    if (effect == null)//��ų ���� �����ڸ�
                    {
                        effectQueue.Dequeue();
                        break;//�ߴ�
                    }
                    else if (effect.trigerInstant == false && effectQueueChecker != 0)//�� ��° ���� ȿ���� ��� ������ �ƴ϶��
                    {
                        break;//�ߴ�
                    }

                    StartCoroutine(EffectTrigerBuffer(effect));//������ ������ ����
                    effectQueue.Dequeue();//ť���� ����
                }
            }
            yield return null;
        }
    }


    public IEnumerator EffectTrigerBuffer(Effect effect)//������ ���� �� ť ����
    {
        if (effect.trigerBlock)//���� ȿ�� ����
        {
            effectQueueChecker++;//ī���� ����
        }

        //����
        if (effect.earlyDelay >= 0f)//������ �ִٸ�
        {
            if (!effect.isMoveOnEarlyDelay)
            {
                effectCanMoveChecker++;
            }
            yield return new WaitForSeconds(effect.earlyDelay);//���
            if (!effect.isMoveOnEarlyDelay)
            {
                effectCanMoveChecker--;
            }
        }

        //ȿ�� ����
        Coroutine coroutine = null;
        switch (effect)
        {
            case Effect_Move effect_move:
                if (canMove)
                {
                    coroutine = StartCoroutine(Dash(effect_move));
                }
                break;
            case Effect_ChangeColor effect_changeColor:
                coroutine = StartCoroutine(ChangeColor(effect_changeColor));
                break;
            case Effect_ChangeMoveSpeed effect_changeMoveSpeed:
                coroutine = StartCoroutine(ChangeMoveSpeed(effect_changeMoveSpeed));
                break;
            case Effect_Shoot effect_Shoot:
                coroutine = StartCoroutine(Shoot(effect_Shoot));
                break;
            default:
                break;
        }
        if (coroutine != null)
        {
            yield return coroutine;
        }

        //�ĵ�
        if (effect.lateDelay >= 0f)//�ĵ��� �ִٸ�
        {
            if (!effect.isMoveOnLateDelay)
            {
                effectCanMoveChecker++;
            }
            yield return new WaitForSeconds(effect.lateDelay);//���
            if (!effect.isMoveOnLateDelay)
            {
                effectCanMoveChecker--;
            }
        }

        if (effect.trigerBlock)//���� ȿ�� ���� ����
        {
            effectQueueChecker--;//ī���� ����
        }
    }

    public IEnumerator Dash(Effect_Move effect)
    {
        //�̵� ���
        effectCanMoveChecker++;
        isDashing = true;

        //���� ����
        movement_Dash = effect.direction.normalized * Mathf.Max(0,effect.speed);
        if (effect.subjectToDirection && !atkSideB)
        {
            movement_Dash.x *= -1;
        }

        //üũ�� ���� ��ġ/�ð�
        Vector2 origin = transform.position;
        float timer = Time.time;

        while (true)
        {
            if (Vector2.Distance(origin, (Vector2)transform.position) >= effect.lifeDistance || Time.time >= timer + effect.duration)//�ð��̳� �Ÿ��� �����Ǹ�
            {
                break;
            }
            yield return null;
        }

        //�̵� ��� ����
        isDashing = false;
        effectCanMoveChecker--;
    }

    public IEnumerator ChangeColor(Effect_ChangeColor effect)
    {
        gameObject.GetComponent<Renderer>().material.color = effect.color;
        yield return new WaitForSeconds(effect.duration);
        gameObject.GetComponent<Renderer>().material.color = colorOrigin;
    }

    public IEnumerator ChangeMoveSpeed(Effect_ChangeMoveSpeed effect)
    {
        speed += effect.increaseAmount;
        yield return new WaitForSeconds(effect.duration);
        speed -= effect.increaseAmount;
    }

    public IEnumerator Shoot(Effect_Shoot effect)
    {
        shooter.bulletGO = effect.prefeb;
        shooter.shootType = effect.shootType;

        shooter.bulletDamageMax = effect.damageMax;
        shooter.bulletDamageMin = effect.damageMin;
        shooter.isHitOnce = effect.isHitOnce;
        shooter.isDestroyAfterHit = effect.isDestroyAfterHit;


        shooter.gameObject.transform.position = (effect.subjectToDirection && !atkSideB)? -(Vector3)effect.offset : (Vector3)effect.offset;
        if (effect.isSubjectToUnitPosition)
        {
            shooter.gameObject.transform.position += transform.position;
        }

        shooter.bulletAngleMax = effect.directionAngleMax;
        shooter.bulletAngleMin = effect.directionAngleMin;
        if (effect.subjectToDirection && !atkSideB)
        {
            shooter.bulletAngleMax *= -1;
            shooter.bulletAngleMin *= -1;
        }

        shooter.bulletSpeedMax = effect.speedMax;
        shooter.bulletSpeedMin = effect.speedMin;
        shooter.lifeDistance = effect.lifeDistance;
        shooter.lifeTime = effect.lifeTime;

        shooter.BulletNum = effect.bulletNum;
        //���ʿ� ����Ʈ�� ���Ϳ� ������ ���� �� ����
        //������ �Ҷ� ���ľ� �� ��

        shooter.triger = true;
        yield return new WaitForSeconds(effect.duration);
    }


    static Queue<Effect> RemoveAll(Queue<Effect> queue, Type valueToRemove)
    {
        Queue<Effect> newQueue = new();

        // ���� Queue�� �ݺ��ϸ鼭 ���� Ŭ������ ������ ��Ҹ� ���ο� Queue�� ����
        foreach (Effect item in queue)
        {
            if (item.GetType() != valueToRemove)
            {
                newQueue.Enqueue(item);
            }
        }

        return newQueue;
    }
}

