using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class Wining : MonoBehaviour
    {
        public TextMeshProUGUI winingName;
        public Image WiningPlayer;

        public void Win(string name, Sprite player)
        {
            AudioManager.instance.PlayMusic(AudioManager.Track.Won, true, 0.5f);
            winingName.text = name + " the winner!!!";
            WiningPlayer.sprite = player;
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void PlayAgain()
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}
