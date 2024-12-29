using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WinningMenu : Menu
    {
        public static WinningMenu instance = null;
        private List<Sprite> playerWin = new List<Sprite>();
        private void Start()
        {
            if (instance)
            {
                Debug.LogError("more than one MainMenu");
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        public TextMeshProUGUI winingName;
        public Image WiningPlayer;

        public void Win(string name, List<Sprite> players)
        {
            maxTimer = Application.targetFrameRate / 60;
            AudioManager.instance.PlayMusic(AudioManager.Track.Won, true, 0.5f);
            winingName.text = name + " the winner!!!";
            playerWin.Clear();
            playerWin = players;
            WiningPlayer.sprite = playerWin[0];
            GameManager.Instance.eventSystem.SetActive(true);
            WinningMenu.instance.TurnOn(null);
        }
        public void BackToMenu()
        {
            Time.timeScale = 1;
            TurnOff(false);
            SceneManager.LoadScene("MainMenu");
        }
        public void PlayAgain()
        {
            Time.timeScale = 1;
            TurnOff(false);
            SceneManager.LoadScene("GamePlay");
        }
        int timer = 0;
        int maxTimer = 0;
        int reverse = 0;
        private void Update()
        {
            if (ROOT.activeSelf)
            {
                timer++;
                if (timer >= maxTimer)
                {
                    timer = 0;
                    int nextsprite = (playerWin.IndexOf(WiningPlayer.sprite) + 1) % playerWin.Count;
                    if (nextsprite == 0) reverse = (reverse + 1) % 2;
                    if (reverse == 0)
                        WiningPlayer.sprite = playerWin[nextsprite];
                    else
                        WiningPlayer.sprite = playerWin[playerWin.Count - 1 - nextsprite];
                }
            }
        }
    }
}
