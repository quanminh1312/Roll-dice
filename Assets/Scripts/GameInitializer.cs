using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts;

namespace Assets
{
    internal class GameInitializer : MonoBehaviour
    {
        public enum GameMode
        {
            INVALID,
            Menus,
            GamePlay
        }
        public GameMode gameMode;
        public GameObject ManagerPrefab = null;
        private bool initialised = false;
        public AudioManager.Track playMusicTrack = AudioManager.Track.None;
        private void Awake()
        {
            if (Manager.Instance == null)
            {
                if (ManagerPrefab)
                {
                    Instantiate(ManagerPrefab);
                }
            }
        }
        private void Update()
        {
            if (!initialised)
            {
                if (gameMode == GameMode.INVALID) return;
                switch (gameMode)
                {
                    case GameMode.Menus:
                        SceneManager.LoadScene("MainMenuMenu", LoadSceneMode.Additive);
                        SceneManager.LoadScene("SelectMenu", LoadSceneMode.Additive);
                        break;
                    case GameMode.GamePlay:
                        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
                        SceneManager.LoadScene("WinningMenu", LoadSceneMode.Additive);
                        break;
                }

                if (playMusicTrack != AudioManager.Track.None)
                {
                    AudioManager.instance.PlayMusic(playMusicTrack, true, 1);
                }
                initialised = true;
            }
        }
    }
}
