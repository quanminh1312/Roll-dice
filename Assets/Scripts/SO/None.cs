using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "none", menuName = "Step/None")]
    internal class None : Step
    {
        public None()
        {
            type = Helper.StepType.None;
        }
        public override Helper.MoveType Execute(out int Value)
        {
            Value = value;
            return Helper.MoveType.None;
        }
    }
}
