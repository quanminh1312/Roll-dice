using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public int playerIndex;
        public string playerName;
        public List<Sprite> playerImages;
        public Sprite playerImage;
        public GameObject playerInfo;


        [HideInInspector] public int steps = 0;
        [HideInInspector] public int currentStep = 1;
        [HideInInspector] private bool isMoving = false;
        [HideInInspector] public bool isTurn = false;
        [HideInInspector] public bool Winning = false;
        [HideInInspector] public bool isDead = false;
        [HideInInspector] public bool doneTurn = false;
        [HideInInspector] public bool canInteract = true;
        [HideInInspector] public bool doneStep = false;
        [HideInInspector] public PlayerData playerData;
        public float timer = 60;
        [HideInInspector] public float timerTurn = 0;
        [HideInInspector] public bool isTimer = false;

        private new SpriteRenderer renderer;
        private static bool AtackMode = false;
        private static int AttackValue = 0;
        [HideInInspector] public bool winCheck = false;
        private void Awake()
        {
            playerData = new PlayerData();
            renderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            playerInfo.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (playerData.healths <= 0 && !isDead)
            {
                isDead = true;
                GameManager.Instance.playerAnimator[playerIndex - 1].SetTrigger("Die");
                //AudioManager.instance.PlaySFX(GameManager.Instance.Hit);
            }
            if (isTurn)
            {
                CheckWinning();
                if (isTimer)
                {
                    timerTurn -= 1 * Time.deltaTime;
                    GameManager.Instance.HandleTimer((int)timerTurn);
                    if (timerTurn <= 0)
                    {
                        timerTurn = timer;
                        GameManager.Instance.RollDice();
                        return;
                    }
                }
                if (steps != 0 && !isMoving && !Winning)
                {
                    StartCoroutine(MoveUp());
                    return;
                }
                if (steps == 0)
                {
                    if (!isMoving && !Winning && !doneStep && !doneTurn && !canInteract)
                    {
                        doneStep = true;
                        GameManager.Instance.HandleStep(this);
                        return;
                    }
                    if (!isMoving && !Winning && doneStep && doneTurn && !canInteract)
                    {
                        GameManager.Instance.EndTurn(this);
                        return;
                    }
                }
                if (Winning && !winCheck)
                {
                    winCheck = true;
                    GameManager.Instance.playerAnimator[playerIndex - 1].SetTrigger("Win");
                    return;
                }
            }
        }
        public void Wining()
        {
            GameManager.Instance.Wining(playerIndex);
        }
        public void Dead()
        {
            playerData.Reset();
            currentStep = 1;
            transform.position = GameManager.Instance.startPos;
            isDead = false;
        }
        public void HandleRoll(int steps)
        {
            if (!isTurn) return;
            this.steps = steps;
            canInteract = false;
            GameManager.Instance.eventSystem.SetActive(false);
        }
        IEnumerator MoveUp()
        {
            if (CheckWinning()) yield break;
            isMoving = true;
            int nextStep;
            if (steps > 0) 
                nextStep = currentStep + 1; 
            else 
                nextStep = currentStep - 1;
            if (nextStep == 0)
            {
                nextStep = 1;
                steps = 0;
                isMoving = false;
                yield break;
            }
            Vector3 nextStepPos = GameManager.Instance.GetStepPosition(nextStep);
            while (transform.position != nextStepPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextStepPos, 0.08f);
                yield return null;
            }
            AudioManager.instance.PlaySFX(GameManager.Instance.jump);
            yield return new WaitForSeconds(0.5f);
            if (steps > 0)
                steps--;
            else
                steps++;
            currentStep = nextStep;
            isMoving = false;
            yield return null;
        }
        bool CheckWinning()
        {
            if (currentStep >= GameManager.Instance.totalSteps)
            {
                Winning = true;
                return true;
            }
            return false;
        }
        public void OnMouseHover()
        {
            string name = "_OutlineEnabled";
            if (renderer.material.HasProperty(name))
            {
                renderer.material.SetFloat(name, 1);
                ChangeText();
                playerInfo.SetActive(true);
            }
        }
        public void OffMouseHover()
        {
            string name = "_OutlineEnabled";
            if (renderer.material.HasProperty(name))
            {
                renderer.material.SetFloat(name, 0);
                playerInfo.SetActive(false);
            }
        }
        public void OnMouseClick()
        {
            if (AtackMode && !isTurn)
            {
                if (AudioManager.instance) AudioManager.instance.PlaySFX(GameManager.Instance.attack);
                AudioManager.instance.PlaySFX(GameManager.Instance.Hit);
                GetAttack(AttackValue);
                GameManager.Instance.DoneAttack();
                AtackMode = false;
                AttackValue = 0;
                return;
            }
            if (Items.onActive)
            {
                switch(Items.itemID)
                {
                    case 0:
                        if (isTurn) return;
                        GetAttack(2);
                        AudioManager.instance.PlaySFX(GameManager.Instance.boom);
                        AudioManager.instance.PlaySFX(GameManager.Instance.Hit);
                        EffectSystem.instance.SpawnSmallExplosion(transform.position);
                        GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].playerData.bombs--;
                        GameManager.Instance.OnChangePlayerData();
                        break;

                    case 1:
                        if (!isTurn) return;
                        if (playerData.potions <= 0) return;
                        if (playerData.healths == playerData.healthsMax) return;
                        AudioManager.instance.PlaySFX(GameManager.Instance.powerUp);
                        playerData.potions--;
                        playerData.healths = Mathf.Clamp(playerData.healths + 2, 0, playerData.healthsMax);
                        GameManager.Instance.OnChangePlayerData();
                        break;
                }
            }
        }
        public void ChangeText()
        {
            playerInfo.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = playerName;
            playerInfo.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.healths.ToString();
            playerInfo.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.shields.ToString();
            playerInfo.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.potions.ToString();
            playerInfo.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.bombs.ToString();
            playerInfo.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.Golds.ToString();
            playerInfo.transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>().text = playerData.skipTurns.ToString();
        }
        public void ChosingPlayerToAttack(int value)
        {
            AtackMode = true;
            AttackValue = value;
        }
        public void GetAttack(int value)
        {
            GameManager.Instance.playerAnimator[playerIndex - 1].SetTrigger("Fall");
            if (playerData.shields >= value)
            {
                playerData.shields -= value;
            }
            else
            {
                playerData.healths = playerData.healths - value + playerData.shields;
                playerData.shields = 0;
            }
        }
    }
}
