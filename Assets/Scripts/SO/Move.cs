using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Step/Move")]
public class Move : Step
{
    public Move()
    {
        type = Helper.StepType.Move;
    }
    public override Helper.MoveType Execute(out int Value)
    {
        Value = value;
        return Helper.MoveType.Move;
    }
}
