using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public string Name;
    public List<Effect> Effects;

}