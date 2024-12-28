using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step : ScriptableObject
{
    public enum MoveType
    {
        Move,
        UseEffect,
        None
    }
    public enum StepType
    {
        Move,
        Attack,
        Defend,
        Heal,
        Skip,
        Trap,
        Collect,
        None
    }
    public enum CollectType
    {
        Gold,
        Potion,
        Bomb,
        None
    }
    [HideInInspector] public StepType type;
    [HideInInspector] public bool used = false;
    public int value;
    
    public abstract MoveType Execute(out int Value);
}
