using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private CharacterType characterType;
    public CharacterType CharacterType
    {
        get
        {
            return characterType;
        }
        set
        {
            characterType = value;
        }
    }

    public bool hitOnce = true;
    public float damage = 1f;

    public void DealDamage(Unit unit)
    {
        if (hitOnce)
        {
            // �Ѿ��� �ı�
            Destroy(gameObject);
        }

        // �����
        unit.Health -= damage;
    }
}
