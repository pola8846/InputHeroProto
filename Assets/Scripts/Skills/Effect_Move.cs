using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_move", menuName = "ScriptableObjects/Effect_move")]

public class Effect_Move : Effect
{
    public Vector2 direction;//���� ����
    public float speed;//�ӵ�
    public float lifeDistance;//��� �Ÿ�. �� �� �ϳ� �̻� �����Ǹ� �׸�
}
