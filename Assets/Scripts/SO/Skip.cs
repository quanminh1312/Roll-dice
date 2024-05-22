using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Skip", menuName = "Step/Skip")]
    internal class Skip : Step
    {
        public Skip()
        {
            type = Helper.StepType.Skip;
        }
        public override Helper.MoveType Execute(out int Value)
        {
            Value = value;
            return Helper.MoveType.None;
        }
    }
}
