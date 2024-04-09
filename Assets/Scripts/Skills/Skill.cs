using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public string Name;
    public List<Effect> Effects;
    public bool isStack;//다른 스킬이 큐에 있어도 올라올 수 있는지
}