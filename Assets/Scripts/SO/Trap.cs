using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Trap", menuName = "Step/Trap")]
    internal class Trap : Step
    {
        public Trap()
        {
            type = Step.StepType.Trap;
        }
        public override Step.MoveType Execute(out int Value)
        {
            Value = value;
            return Step.MoveType.UseEffect;
        }
    }
}
