using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Choose
    {
        public TMPro.TextMeshProUGUI text;
        public Image image;
        public TMPro.TMP_InputField inputField;
        public Button nextButton;
        public Button previousButton;
        public bool active = false;
        public void InActive()
        {
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(false);
            active = false;
        }
        public void Active()
        {
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(true);
            active = true;
        }
    }
    public class SelectMenu : Menu
    {
        public static SelectMenu instance = null;
        public List<Choose> PlayerChooses = null;
        public Button addButton = null;
        public List<Sprite> ButtonImage = null;
        bool addButtonPressed = false;
        public List<Sprite> PlayerSprite = new List<Sprite>();
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
        public void ReturnToPreviousMenu()
        {
            TurnOff(true);
        }
        public void ToGamePlayScene()
        {
            TurnOff(false);
            addPlayer();
            SceneManager.LoadScene("GamePlay");
        }
        public override void TurnOn(Menu previous)
        {
            foreach (var item in PlayerChooses)
            {
                item.Active();
                item.image.sprite = PlayerSprite[PlayerChooses.IndexOf(item)];

            }
            addButtonPressed = true; AddPlayer3();
            base.TurnOn(previous);
        }
        public void AddPlayer3()
        {
            if (!addButtonPressed)
            {
                addButtonPressed = true;
                addButton.image.sprite = ButtonImage[1];
                PlayerChooses[2].Active();
                return;
            }
            else
            {
                addButtonPressed = false;
                addButton.image.sprite = ButtonImage[0];
                PlayerChooses[2].InActive();
                return;
            }
        }
        public void Next(int index)
        {
            int next = (PlayerSprite.IndexOf(PlayerChooses[index].image.sprite) + 1) % PlayerSprite.Count;
            PlayerChooses[index].image.sprite = PlayerSprite[next];
            foreach (var item in PlayerChooses)
            {
                if (PlayerSprite.IndexOf(item.image.sprite) == next && item != PlayerChooses[index])
                {
                    item.image.sprite = PlayerSprite[(next + PlayerSprite.Count - 1) % PlayerSprite.Count];
                    break;
                }
            }
        }
        public void Previous(int index)
        {
            int next = (PlayerSprite.IndexOf(PlayerChooses[index].image.sprite) + PlayerSprite.Count - 1) % PlayerSprite.Count;
            PlayerChooses[index].image.sprite = PlayerSprite[next];
            foreach (var item in PlayerChooses)
            {
                if (PlayerSprite.IndexOf(item.image.sprite) == next)
                {
                    PlayerChooses[index].image.sprite = PlayerSprite[(next + 1) % PlayerSprite.Count];
                    break;
                }
            }
        }
        void addPlayer()
        {
            foreach (var item in PlayerChooses)
            {
                if (item.active)
                {
                    if (item.inputField.text == "")
                    {
                        Helper.AddPlayer(PlayerSprite.IndexOf(item.image.sprite), "Player " + (PlayerChooses.IndexOf(item) + 1));
                    }
                    else
                    {
                        Helper.AddPlayer(PlayerSprite.IndexOf(item.image.sprite), item.inputField.text);
                    }
                }
            }
        }
    }
}
