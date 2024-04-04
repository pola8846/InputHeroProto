using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private CharacterType characterType;
    [SerializeField]
    private Unit unit;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //공격인지 체크
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            //누구 총알/히트박스인지 체크
            switch (characterType)
            {
                case CharacterType.player:
                    if (projectile.CharacterType == CharacterType.enemy)//적 총알이면
                    {
                        projectile.DealDamage(unit);
                    }
                    break;
                case CharacterType.enemy:
                    if (projectile.CharacterType == CharacterType.player)//플레이어 총알이면
                    {
                        projectile.DealDamage(unit);
                    }
                    break;
                default:
                    break;
            }
        }

        DamageArea area = collision.GetComponent<DamageArea>();
        if (area != null)
        {
            switch (characterType)
            {
                case CharacterType.player:
                    if (area.CharacterType == CharacterType.enemy)//적 총알이면
                    {
                        area.DealDamage(unit);
                    }
                    break;
                case CharacterType.enemy:
                    if (area.CharacterType == CharacterType.player)//플레이어 총알이면
                    {
                        area.DealDamage(unit);
                    }
                    break;
                default:
                    break;
            }
        }
        
        
    }
}

public enum CharacterType
{
    player,
    enemy,
}