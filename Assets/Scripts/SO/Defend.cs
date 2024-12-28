using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Defend", menuName = "Step/Defend")]
    internal class Defend: Step
    {
        public Defend()
        {
            type = Step.StepType.Defend;
        }
        public override Step.MoveType Execute(out int Value)
        {
            Value = value;
            return Step.MoveType.UseEffect;
        }
    }
}
