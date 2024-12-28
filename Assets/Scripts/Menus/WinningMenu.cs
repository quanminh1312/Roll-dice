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

        public void Win(string name, Sprite player)
        {
            AudioManager.instance.PlayMusic(AudioManager.Track.Won, true, 0.5f);
            winingName.text = name + " the winner!!!";
            WiningPlayer.sprite = player;
            GameManager.Instance.eventSystem.SetActive(true);
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
