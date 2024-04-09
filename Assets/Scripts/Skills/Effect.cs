using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Effect : ScriptableObject
{
    [Header("�ߵ� �ð� ����")]
    public float duration = 0;//���ӽð�(ȿ���� �����Ǿ��� ġ��)
    public bool trigerInstant = true;//���� ȿ���� ���ÿ� �����ϴ���. false�� ���� ������ ��ٸ�
    public bool trigerBlock = true;//���� ȿ���� ���� �� �ִ���. false�� ȿ�� �ߵ� �߿��� 

    public float earlyDelay = 0;//����
    public bool isMoveOnEarlyDelay = true;//���� ���� ������ �� �ִ���
    public float lateDelay = 0;//�ĵ�
    public bool isMoveOnLateDelay = true;//�ĵ� ���� ������ �� �ִ���

    [Header("�ߵ� ����")]
    public bool subjectToDirection = false;//���� ���⿡ �����ϴ���

    [Header("�ߵ� ����")]
    public float needStayTime = 0;//up�� ������, �ش� �ð� ���� stay�� ������ �־���� �ߵ�

}
