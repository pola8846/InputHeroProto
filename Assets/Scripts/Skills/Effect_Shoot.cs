using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Effect_shoot", menuName = "ScriptableObjects/Effect_shoot")]
public class Effect_Shoot : Effect
{
    [Header("����ü ������")]
    public GameObject prefeb;//����ü(Ȥ�� ��������)������
    public BulletShootType shootType;//�߻� ���

    [Header("�����")]
    public float damageMin;//�����
    public float damageMax;//�����
    public bool isHitOnce;//�� ���� �浹 �����Ѱ�?//�̱���
    public bool isDestroyAfterHit;//�浹 �� ������°�?//�̱���

    [Header("�߻�")]
    public Vector2 offset;//ź �߻� ���� ��ġ
    public bool isSubjectToUnitPosition = true;//���� ��ġ ��������, ������ǥ����
    public float directionAngleMin;//ź �߻� ����
    public float directionAngleMax;//ź �߻� ����

    [Header("�ӵ�")]
    public float speedMin;//�ӵ�
    public float speedMax;//�ӵ�
    public float lifeTime;//ź ����
    public float lifeDistance;//ź �ִ� �Ÿ�. �� �̻� ������ �����
}
