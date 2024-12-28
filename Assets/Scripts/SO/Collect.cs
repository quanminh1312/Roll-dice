using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Collect", menuName = "Step/Collect")]
    public class Collect : Step
    {
        [SerializeField]
        public Step.CollectType collectType;
        public Collect()
        {
            type = Step.StepType.Collect;
        }
        public override Step.MoveType Execute(out int Value)
        {
            Value = value;
            return Step.MoveType.None;
        }
    }
}
