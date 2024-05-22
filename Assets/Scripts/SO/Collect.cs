using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "Collect", menuName = "Step/Collect")]
    internal class Collect : Step
    {
        [SerializeField]
        public Helper.CollectType collectType;
        public Collect()
        {
            type = Helper.StepType.Collect;
        }
        public override Helper.MoveType Execute(out int Value)
        {
            Value = value;
            return Helper.MoveType.None;
        }
    }
}
