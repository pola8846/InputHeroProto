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
    private int effectQueueChecker = 0;//발동 중인 효과 스택. 0이여야 새 효과를 실행
    [SerializeField]
    private int effectCanMoveChecker = 0;//이동 불가능 한 효과 스택. 0이여야 이동 가능
    [Header("이동")]
    [SerializeField]
    private float speed = 3f;
    private float movement = 0;
    private Vector2 movement_Dash;

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
                case SkillTiming.Down://누를 때
                case SkillTiming.Up://뗄 때
                    AddEffectAtQueue(effect);
                    break;

                case SkillTiming.Stay://누르기 시작할 때
                    if (!stopStay)//누를 때
                    {
                        AddEffectAtQueue(effect);
                    }
                    else//뗄 때
                    {
                        RemoveAll(effectQueue, effect.GetType());//큐에서 효과 제거
                        //효과 정지 넣어야 함
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

    public void AddEffectAtQueue(Effect effect)//큐에 효과 추가. 
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
            if (effectQueueChecker == 0)//효과가 실행 중이 아니면
            {
                while (effectQueue.Count >= 1)//실행 대기 중인 효과가 있으면 구분자나 즉시 실행이 false인 효과가 있을 때까지 실행
                {
                    Effect effect = effectQueue.Peek();
                    if (effect == null)//스킬 사이 구분자면
                    {
                        effectQueue.Dequeue();
                        break;//중단
                    }
                    else if (effect.trigerInstant == false && effectQueueChecker != 0)//두 번째 이후 효과가 즉시 실행이 아니라면
                    {
                        break;//중단
                    }

                    StartCoroutine(EffectTrigerBuffer(effect));//딜레이 감안한 실행
                    effectQueue.Dequeue();//큐에서 제거
                }
            }
            yield return null;
        }
    }


    public IEnumerator EffectTrigerBuffer(Effect effect)//딜레이 구현 및 큐 실행
    {
        if (effect.trigerBlock)//다음 효과 막기
        {
            effectQueueChecker++;//카운터 증가
        }

        //선딜
        if (effect.earlyDelay >= 0f)//선딜이 있다면
        {
            if (!effect.isMoveOnEarlyDelay)
            {
                effectCanMoveChecker++;
            }
            yield return new WaitForSeconds(effect.earlyDelay);//대기
            if (!effect.isMoveOnEarlyDelay)
            {
                effectCanMoveChecker--;
            }
        }

        //효과 실행
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

        //후딜
        if (effect.lateDelay >= 0f)//후딜이 있다면
        {
            if (!effect.isMoveOnLateDelay)
            {
                effectCanMoveChecker++;
            }
            yield return new WaitForSeconds(effect.lateDelay);//대기
            if (!effect.isMoveOnLateDelay)
            {
                effectCanMoveChecker--;
            }
        }

        if (effect.trigerBlock)//다음 효과 막기 해제
        {
            effectQueueChecker--;//카운터 감소
        }
    }

    public IEnumerator Dash(Effect_Move effect)
    {
        //이동 잠금
        effectCanMoveChecker++;
        isDashing = true;

        //방향 설정
        movement_Dash = effect.direction.normalized * Mathf.Max(0,effect.speed);
        if (effect.subjectToDirection && !atkSideB)
        {
            movement_Dash.x *= -1;
        }

        //체크용 시작 위치/시간
        Vector2 origin = transform.position;
        float timer = Time.time;

        while (true)
        {
            if (Vector2.Distance(origin, (Vector2)transform.position) >= effect.lifeDistance || Time.time >= timer + effect.duration)//시간이나 거리가 충족되면
            {
                break;
            }
            yield return null;
        }

        //이동 잠금 해제
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
        //애초에 이펙트를 슈터에 넣으면 됐을 것 같음
        //다음에 할땐 고쳐야 할 것

        shooter.triger = true;
        yield return new WaitForSeconds(effect.duration);
    }


    static Queue<Effect> RemoveAll(Queue<Effect> queue, Type valueToRemove)
    {
        Queue<Effect> newQueue = new();

        // 원본 Queue를 반복하면서 같은 클래스를 제거한 요소를 새로운 Queue에 저장
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

