using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "attack", menuName = "Step/Attack")]
    internal class Attack : Step
    {
        enum AttackType
        {
            GoBack,
            Heal,
            None
        }
        public Attack()
        {
            type = Helper.StepType.Attack;
        }
        public override Helper.MoveType Execute(out int Value)
        {
            Value = value;
            return Helper.MoveType.UseEffect;
        }
    }
}
