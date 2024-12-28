using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "heal", menuName = "Step/Heal")]
    internal class Heal : Step
    {
        public Heal()
        {
            type = Step.StepType.Heal;
        }
        public override Step.MoveType Execute(out int Value)
        {
            Value = value;
            return Step.MoveType.UseEffect;
        }
    }
}
