using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_ChangeMoveSpeed", menuName = "ScriptableObjects/Effect_ChangeMoveSpeed")]
public class Effect_ChangeMoveSpeed : Effect
{
    public float increaseAmount;//속도변화량
    public float lifeTime;//지속시간
}
