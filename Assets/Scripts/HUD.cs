using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HUD : MonoBehaviour
    {
        [HideInInspector] public Image playerImage;
        public TextMeshProUGUI playerName;
        public List<GameObject> playerHealth;
        public TextMeshProUGUI playerShields;
        public TextMeshProUGUI playerPotions;
        public TextMeshProUGUI playerGolds;
        public TextMeshProUGUI playerBombs;

        public TextMeshProUGUI playerTimer;
        private GraphicRaycaster raycaster;
        private void OnEnable()
        {
            //GameManager.Instance.OnPlayerData += HandlePlayerDataChange;
            //GameManager.Instance.OnPlayerTimer += HandelTimer;
            //GameManager.Instance.OnHUD += HandleRayCast;
        }
        private void Start()
        {
            raycaster = GetComponent<GraphicRaycaster>();
            raycaster.enabled = true;
        }
        public void HandleRayCast(bool isRayCast)
        {
            raycaster.enabled = isRayCast;
        }
        public void HandlePlayerDataChange(Player player)
        {
            playerImage.sprite = player.playerImage;
            playerName.text = player.playerName + " Turn's";
            playerShields.text = "x" + player.playerData.shields.ToString();
            playerPotions.text = "x" + player.playerData.potions.ToString();
            playerGolds.text = "x" + player.playerData.Golds.ToString();
            playerBombs.text = "x" + player.playerData.bombs.ToString();
            for (int i = 0; i < playerHealth.Count; i++)
            {
                if (i < player.playerData.healths)
                {
                    playerHealth[i].gameObject.SetActive(true);
                }
                else
                {
                    playerHealth[i].gameObject.SetActive(false);
                }
            }
        }
        public void HandleTimer(int timer)
        {
            playerTimer.text = timer.ToString();
        }
        public void HandleOnBuying(int Id)
        {
            GameManager.Instance.HandleBuying(Id);
        }
    }
}
