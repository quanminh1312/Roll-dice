using Assets.Scripts.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class StepCol : MonoBehaviour
    {
        public Step stepConfig;
        [SerializeField] new SpriteRenderer renderer;
        public TextMeshProUGUI Text;

        private void Start()
        {
            if (stepConfig.type != Step.StepType.None)
            {
                if (stepConfig.type == Step.StepType.Trap)
                {
                    renderer.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Text.text = stepConfig.value.ToString();
                }
                if ((int)stepConfig.type != 6)
                    renderer.sprite = GameManager.Instance.sprites[(int)stepConfig.type];
                else
                    renderer.sprite = GameManager.Instance.sprites[6 + (int)((Collect)stepConfig).collectType];
            }
        }
    }
}
