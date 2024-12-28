using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Step/Move")]
public class Move : Step
{
    public Move()
    {
        type = Step.StepType.Move;
    }
    public override Step.MoveType Execute(out int Value)
    {
        Value = value;
        return Step.MoveType.Move;
    }
}
