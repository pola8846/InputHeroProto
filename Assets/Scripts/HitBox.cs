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

        //�������� üũ
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            //���� �Ѿ�/��Ʈ�ڽ����� üũ
            switch (characterType)
            {
                case CharacterType.player:
                    if (projectile.CharacterType == CharacterType.enemy)//�� �Ѿ��̸�
                    {
                        projectile.DealDamage(unit);
                    }
                    break;
                case CharacterType.enemy:
                    if (projectile.CharacterType == CharacterType.player)//�÷��̾� �Ѿ��̸�
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
                    if (area.CharacterType == CharacterType.enemy)//�� �Ѿ��̸�
                    {
                        area.DealDamage(unit);
                    }
                    break;
                case CharacterType.enemy:
                    if (area.CharacterType == CharacterType.player)//�÷��̾� �Ѿ��̸�
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