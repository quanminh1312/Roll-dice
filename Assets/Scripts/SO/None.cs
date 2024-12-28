using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "none", menuName = "Step/None")]
    internal class None : Step
    {
        public None()
        {
            type = Step.StepType.None;
        }
        public override Step.MoveType Execute(out int Value)
        {
            Value = value;
            return Step.MoveType.None;
        }
    }
}
