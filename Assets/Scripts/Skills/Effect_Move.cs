using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_move", menuName = "ScriptableObjects/Effect_move")]

public class Effect_Move : Effect
{
    public Vector2 direction;//진행 방향
    public float speed;//속도
    public float lifeDistance;//대시 거리. 둘 중 하나 이상 충족되면 그만
}
