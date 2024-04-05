using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Effect_shoot", menuName = "ScriptableObjects/Effect_shoot")]
public class Effect_Shoot : Effect
{
    public GameObject prefeb;//투사체(혹은 판정범위)프리팹
    public float damage;//대미지
    public bool isHitOnce;//한 번만 충돌 가능한가?
    public bool isDestroyAfterHit;//충돌 후 사라지는가?
    public Vector2 offset;//탄 발사 시작 위치
    public Vector2 direction;//탄 발사 진행 위치
    public float speed;//속도
    public float lifeTime;//탄 수명
    public float lifeDistance;//탄 최대 거리. 이 이상 지나면 사라짐
}
