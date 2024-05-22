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
                    Helper.Init();
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
                        MenuManager.instance.SwitchToMainMenuMenus();
                        //GameManager.Instance.gameState = GameManager.GameState.InMenus;
                        break;
                    case GameMode.GamePlay:
                        MenuManager.instance.SwitchToGameplayMenus();
                        //GameManager.Instance.gameState = GameManager.GameState.Playing;
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
