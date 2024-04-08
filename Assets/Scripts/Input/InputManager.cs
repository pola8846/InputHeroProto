using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    [SerializedDictionary("Action", "Input")]
    private SerializedDictionary<KeyCode, InputKey> inputs = new();

    public SerializedDictionary<KeyCode, SerializedDictionary<SkillTiming, Skill>> skills = new();
    private void Start()
    {
        GameManager.SetManager(this);
    }

    private void Update()
    {
        bool isMoving = false;
        foreach (var input in inputs)
        {
            if (!input.Value.isTrigerOnce && Input.GetKey(input.Value.key))
            {
                if (input.Value.action == Action.MoveLeft || input.Value.action == Action.MoveRight)
                {
                    isMoving = true;
                }
                GameManager.Player2D.DoAction(input.Value.action);
            }
            if (input.Value.isTrigerOnce && Input.GetKeyDown(input.Value.key))
            {
                if (input.Value.action == Action.MoveLeft || input.Value.action == Action.MoveRight)
                {
                    isMoving = true;
                }
                GameManager.Player2D.DoAction(input.Value.action);
            }
        }
        if (!isMoving)
        {
            GameManager.Player2D.DoAction(Action.MoveStop);
        }

        foreach (var margedSkill in skills)
        {
            if (Input.GetKeyDown(margedSkill.Key))//누를 때
            {
                if (margedSkill.Value.ContainsKey(SkillTiming.Down))
                {
                    GameManager.Player2D.DoAction(SkillTiming.Down, margedSkill.Value[SkillTiming.Down]);
                }
            }
            else if (Input.GetKeyUp(margedSkill.Key))//뗄 때
            {
                if (margedSkill.Value.ContainsKey(SkillTiming.Up))
                {
                    GameManager.Player2D.DoAction(SkillTiming.Up, margedSkill.Value[SkillTiming.Up]);
                }
                if (margedSkill.Value.ContainsKey(SkillTiming.Stay))
                {
                    GameManager.Player2D.DoAction(SkillTiming.Stay, margedSkill.Value[SkillTiming.Stay], true);
                }
            }
            else if (Input.GetKey(margedSkill.Key))//누르고 있을 때
            {
                if (margedSkill.Value.ContainsKey(SkillTiming.Stay))
                {
                    GameManager.Player2D.DoAction(SkillTiming.Stay, margedSkill.Value[SkillTiming.Stay], false);
                }
            }
        }

    }
}

[Serializable]
public struct InputKey
{
    public KeyCode key;//키 코드
    public Action action;//실행할 행위
    public bool isActivated;//먹고 있는지
    public bool isTrigerOnce;//한 번만 실행되는지

    public InputKey(KeyCode _key, Action _action, bool _isActivated = true, bool _isTrigerOnce = true)
    {
        key = _key;
        isActivated = _isActivated;
        action = _action;
        isTrigerOnce = _isTrigerOnce;
    }
}



[Serializable]
public enum Action
{
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    MoveStop,
    Jump,
    AttackRange,
    AttackMelee,
    DashLeft,
    DashRight,

}

[Serializable]
public enum SkillTiming
{
    Down,
    Stay,
    Up,
}