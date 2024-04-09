using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Effect_shoot", menuName = "ScriptableObjects/Effect_shoot")]
public class Effect_Shoot : Effect
{
    [Header("����ü ������")]
    public GameObject prefeb;//����ü(Ȥ�� ��������)������
    public int bulletNum = 1;//�Ѿ� ����
    public BulletShootType shootType;//�߻� ���

    [Header("�����")]
    public float damageMin = 1;//�����
    public float damageMax = 1;//�����
    public bool isHitOnce = true;//�� ���� �浹 �����Ѱ�?//�̱���
    public bool isDestroyAfterHit = true;//�浹 �� ������°�?//�̱���

    [Header("�߻�")]
    public Vector2 offset = Vector2.zero;//ź �߻� ���� ��ġ
    public bool isSubjectToUnitPosition = true;//���� ��ġ ��������, ������ǥ����
    public float directionAngleMin = -90;//ź �߻� ����
    public float directionAngleMax = -90;//ź �߻� ����

    [Header("�ӵ�")]
    public float speedMin = 1;//�ӵ�
    public float speedMax = 1;//�ӵ�
    public float lifeTime = 0;//ź ����
    public float lifeDistance = 0;//ź �ִ� �Ÿ�. �� �̻� ������ �����
}
