using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Effect : ScriptableObject
{
}

public enum InputType
{
    Down,
    Stay,
    Up,
    Passive,
}