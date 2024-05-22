using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Defend", menuName = "Step/Defend")]
    internal class Defend: Step
    {
        public Defend()
        {
            type = Helper.StepType.Defend;
        }
        public override Helper.MoveType Execute(out int Value)
        {
            Value = value;
            return Helper.MoveType.UseEffect;
        }
    }
}
