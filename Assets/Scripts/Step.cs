using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step : ScriptableObject
{

    [HideInInspector] public Helper.StepType type;
    [HideInInspector] public bool used = false;
    public int value;
    
    public abstract Helper.MoveType Execute(out int Value);
}
