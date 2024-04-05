using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Effect_shoot", menuName = "ScriptableObjects/Effect_shoot")]
public class Effect_Shoot : Effect
{
    public GameObject prefeb;//����ü(Ȥ�� ��������)������
    public float damage;//�����
    public bool isHitOnce;//�� ���� �浹 �����Ѱ�?
    public bool isDestroyAfterHit;//�浹 �� ������°�?
    public Vector2 offset;//ź �߻� ���� ��ġ
    public Vector2 direction;//ź �߻� ���� ��ġ
    public float speed;//�ӵ�
    public float lifeTime;//ź ����
    public float lifeDistance;//ź �ִ� �Ÿ�. �� �̻� ������ �����
}
