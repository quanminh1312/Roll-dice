using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Manager : MonoBehaviour
    {
        public static Manager Instance = null;
        private void Start()
        {
            if (Instance)
            {
                Debug.LogError("more than 1 manager");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
