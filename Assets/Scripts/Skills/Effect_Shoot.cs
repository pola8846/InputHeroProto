using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Effect_shoot", menuName = "ScriptableObjects/Effect_shoot")]
public class Effect_Shoot : Effect
{
    [Header("투사체 프리팹")]
    public GameObject prefeb;//투사체(혹은 판정범위)프리팹
    public BulletShootType shootType;//발사 방식

    [Header("대미지")]
    public float damageMin;//대미지
    public float damageMax;//대미지
    public bool isHitOnce;//한 번만 충돌 가능한가?//미구현
    public bool isDestroyAfterHit;//충돌 후 사라지는가?//미구현

    [Header("발사")]
    public Vector2 offset;//탄 발사 시작 위치
    public bool isSubjectToUnitPosition = true;//유닛 위치 기준인지, 절대좌표인지
    public float directionAngleMin;//탄 발사 각도
    public float directionAngleMax;//탄 발사 각도

    [Header("속도")]
    public float speedMin;//속도
    public float speedMax;//속도
    public float lifeTime;//탄 수명
    public float lifeDistance;//탄 최대 거리. 이 이상 지나면 사라짐
}
