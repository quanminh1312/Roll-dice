using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static Helper Instance { get; private set; }
    public static void Init()
    {
        Instance = new Helper();
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
    public enum MoveType
    {
        Move,
        UseEffect,
        None
    }
    public enum CollectType
    {
        Gold,
        Potion,
        Bomb,
        None
    }
}
