using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Effect : ScriptableObject
{
    [Header("발동 시간 관련")]
    public float duration = 0;//지속시간(효과가 유지되었다 치는)
    public bool trigerInstant = true;//이전 효과와 동시에 실행하는지. false면 끝날 때까지 기다림
    public bool trigerBlock = true;//다음 효과를 막을 수 있는지. false면 효과 발동 중에도 

    public float earlyDelay = 0;//선딜
    public bool isMoveOnEarlyDelay = true;//선딜 도중 움직일 수 있는지
    public float lateDelay = 0;//후딜
    public bool isMoveOnLateDelay = true;//후딜 도중 움직일 수 있는지

    [Header("발동 방향")]
    public bool subjectToDirection = false;//유닛 방향에 의존하는지

    [Header("발동 조건")]
    public float needStayTime = 0;//up에 놓으면, 해당 시간 동안 stay를 누르고 있었어야 발동

}
