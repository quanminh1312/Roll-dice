﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.SO;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        //[HideInInspector] public event Action<Player> OnPlayerData;
        //[HideInInspector] public event Action<int> OnPlayerTimer;
        [HideInInspector] public event Action OnRollDices;
        //[HideInInspector] public event Action<bool> OnHUD;
        public int totalSteps = 55;
        public int row = 6;
        public int offset = 1;
        public int posOffset = 1;
        public Vector3 startPos = new Vector3(-7.5f, 1.5f, 0);
        public GameObject stepPrefab;
        public List<Step> steps = new List<Step>();
        public List<Sprite> sprites = new List<Sprite>();
        public GameObject rollDice;
        public GameObject playerGroup;
        public List<GameObject> playerPrefabs = new List<GameObject>();
        public List<Player> players = new List<Player>();
        public int playerPlay = 3;
        public GameObject eventSystem;
        public GameObject mouseHover;
        public AudioClip boom;
        public AudioClip powerUp;
        public AudioClip coin;
        public AudioClip attack;
        public AudioClip jump;
        public AudioClip NextTurn;
        public AudioClip Hit;
        public HUD hud;
        [HideInInspector] public int currentPlayer = 0;
        [HideInInspector] public List<StepCol> gameSteps = new List<StepCol>();
        [HideInInspector] public List<Animator> playerAnimator = new List<Animator>();
        public enum GameState
        {
            INVALID,
            InMenus,
            Playing,
            Paused,
        }
        public GameState gameState = GameState.INVALID;
        public static GameManager Instance = null;
        private void Awake()
        {
            // destroy old instance if exist
            if (Instance != null)
            {
                //delete all player and playerGameObjects
                for (int i = 0; i < players.Count; i++)
                {
                    Destroy(players[i].gameObject);
                }
                players.Clear();
                Destroy(Instance);
            }
            Instance = this;
        }
        private void Start()
        {
            Application.targetFrameRate = 60;
            AddPlayer();
            hud.playerName.text = players[0].playerName + " Turn's";
            eventSystem.SetActive(true);
            Play();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        public void AddPlayer()
        {
            for (int i = 0; i < Helper.playerIndex.Count; i++)
            {
                //instatiate player and set parent and set transform at 0 , 0 , 0
                Player player = Instantiate(playerPrefabs[Helper.playerIndex[i]]).GetComponent<Player>();
                player.transform.SetParent(playerGroup.transform);
                player.transform.localPosition = Vector3.zero;
                player.playerIndex = players.Count + 1;
                player.playerName = Helper.playerNames[i];
                players.Add(player);
            }
            playerPlay = players.Count;
        }
        public void Wining(int index)
        {
            eventSystem.SetActive(true);
            WinningMenu.instance.Win(players[index - 1].playerName, players[index - 1].playerImages);
            Time.timeScale = 0;
        }
        public Vector3 GetStepPosition(int step)
        {
            int currentColumn = step / (row + offset) + 1;
            int currentRow = step % (row + offset);
            float x = 0, y = 0;
            if (currentColumn % 2 == 0)
            {
                x =  row - currentRow + 1;
                y = currentColumn;
                if (currentRow == 0)
                {
                    x -= 1;
                    y -= 0.5f;
                }
            }
            else
            {
                x = currentRow; 
                y = currentColumn;
                if (x == 0)
                {
                    x = 1;
                    y -= 0.5f;
                }
            }
            x = (x-1) * posOffset;
            y = (y-1) * (posOffset*2);
            return new Vector3(startPos.x + y, startPos.y - x, startPos.z);
        }

        public void SpawnSteps()
        {
            bool[] checkSteps = new bool[totalSteps];
            for (int i = 2; i < totalSteps; i++)
            {
                StepCol step = Instantiate(stepPrefab).GetComponent<StepCol>();
                step.transform.position = GetStepPosition(i);
                if (UnityEngine.Random.Range(0, 2) == 0 && !checkSteps[i])
                {
                    step.stepConfig = steps[UnityEngine.Random.Range(0, 6)];
                    int t = step.stepConfig.value;
                    for (int j = i+1; j <= Math.Min(totalSteps-1,i+t); j++)
                    {
                        checkSteps[j] = true;
                    }
                }
                else
                    step.stepConfig = steps[UnityEngine.Random.Range(6, steps.Count)];
                gameSteps.Add(step);
            }
        }

        public void Play()
        {
            StartCoroutine(PlayWait());
        }
        IEnumerator PlayWait()
        {
            yield return new WaitForSeconds(0.5f);
            SpawnSteps();
            foreach (var player in players)
            {
                EndPlayerTurn(player);
                player.isDead = false;
                player.winCheck = false;
                player.playerData.Reset();
                playerAnimator.Add(player.GetComponent<Animator>());
            }
            Turn(players[0]);
            players[0].isTimer = true;
            //players[0].steps = 57;
            yield return null;
        }
        public void OnRollDice()
        {
            players[currentPlayer - 1].isTimer = false;
            rollDice.gameObject.SetActive(true);
        }
        public void RollDice()
        {
            OnRollDices?.Invoke();
        }
        public void HandleRoll(int result)
        {
            players[currentPlayer - 1].HandleRoll(result);
        }

        #region endTurn
        public void EndPlayerTurn(Player player)
        {
            player.isTurn = false;
            player.doneTurn = true;
            player.doneStep = true;
            player.canInteract = false;
            player.steps = 0;
            player.timerTurn = 0;
        }
        public void EndTurn(Player player)
        {
            EndPlayerTurn(player);
            int index = player.playerIndex - 1;
            index = (index + 1) % playerPlay;
            players[index].isTimer = true;
            StartCoroutine(EndTurnWait(index));
        }
        IEnumerator EndTurnWait(int index)
        {
            yield return new WaitForSeconds(1.5f); 
            Turn(players[index]);
            AudioManager.instance.PlaySFX(NextTurn);
            yield return null;
        }
        #endregion

        #region OnTurn
        public void OnPlayerTurn(Player player)
        {
            player.doneTurn = false;
            player.doneStep = false;
            player.canInteract = true;
            player.isTurn = true;
            player.timerTurn = player.timer;
            currentPlayer = player.playerIndex;
        }
        public void Turn(Player player)
        {
            if (player.playerData.skipTurns > 0)
            {
                player.playerData.skipTurns--;
                //OnPlayerData?.Invoke(player);
                hud.HandlePlayerDataChange(player);
                EndTurn(player);
                return;
            }
            OnPlayerTurn(player);
            eventSystem.SetActive(true);
            //OnPlayerData?.Invoke(player);
            hud.HandlePlayerDataChange(player);
        }
        #endregion

        public void HandleTimer(int timer)
        {
            //OnPlayerTimer?.Invoke(timer);
            hud.HandleTimer(timer);
        }
        public bool ToggleHUDRaycast(bool isOn)
        {
            //OnHUD?.Invoke(isOn);
            hud.HandleRayCast(isOn);
            return isOn;
        }
        public void OnChangePlayerData()
        {
            //OnPlayerData?.Invoke(players[currentPlayer - 1]);
            hud.HandlePlayerDataChange(players[currentPlayer - 1]);
        }
        public void HandleBuying(int Id)
        {
            int playerIndex = currentPlayer - 1;
            switch (Id)
            {
                case 0:
                    //buy shield
                    if (players[playerIndex].playerData.Golds >= 1 && players[playerIndex].playerData.shields < players[playerIndex].playerData.shieldsMax)
                    {
                        players[playerIndex].playerData.Golds -= 1;
                        players[playerIndex].playerData.shields++;
                    }
                    break;
                case 1:
                    //buy potion
                    if (players[playerIndex].playerData.Golds >= 1)
                    {
                        players[playerIndex].playerData.Golds -= 1;
                        players[playerIndex].playerData.potions++;
                    }
                    break;
                case 2:
                    //buy bomb
                    if (players[playerIndex].playerData.Golds >= 1)
                    {
                        players[playerIndex].playerData.Golds -= 1;
                        players[playerIndex].playerData.bombs++;
                    }
                    break;
            }
            //OnPlayerData?.Invoke(players[playerIndex]);
            hud.HandlePlayerDataChange(players[playerIndex]);
        }

        #region StepsEffect
        public void HandleStep(Player player)
        {
            if (player.currentStep <= 1)
            {
                player.doneStep = true;
                player.doneTurn = true;
                return;
            }
            Step step = gameSteps[player.currentStep - 2].stepConfig;
            int value;
            Step.MoveType moveType = step.Execute(out value);
            switch(moveType)
            {
                case Step.MoveType.Move:
                    player.steps = value;
                    player.doneStep = false;
                    break;

                case Step.MoveType.UseEffect:
                    UseEffect(player, step);
                    player.doneStep = true;
                    player.doneTurn = true;
                    break;

                case Step.MoveType.None:
                    CollectSteps(player, step);
                    player.doneStep = true;
                    player.doneTurn = true;
                    break;
            }
        }
        public void UseEffect(Player player, Step step)
        {
            if (step.type == Step.StepType.Trap)
            {
                EffectSystem.instance.SpawnSmallExplosion(player.gameObject.transform.position);
                if (AudioManager.instance) AudioManager.instance.PlaySFX(boom);
                if (AudioManager.instance) AudioManager.instance.PlaySFX(Hit);
                playerAnimator[player.playerIndex - 1].SetTrigger("Fall");
                if (player.playerData.shields >= 2)
                {
                    player.playerData.shields -= 2;
                }
                else
                {
                    player.playerData.healths = player.playerData.healths - 2 + player.playerData.shields;
                    player.playerData.shields = 0;
                }
            }
            else if (step.type == Step.StepType.Defend)
            {
                player.playerData.shields = Mathf.Clamp(player.playerData.shields + step.value, 0, player.playerData.shieldsMax);
                if (AudioManager.instance) AudioManager.instance.PlaySFX(powerUp);
            }
            else if (step.type == Step.StepType.Heal)
            {
                player.playerData.healths = Mathf.Clamp(player.playerData.healths + step.value, 0, player.playerData.healthsMax);
                if (AudioManager.instance) AudioManager.instance.PlaySFX(powerUp);
            }
            else if (step.type == Step.StepType.Attack)
            {
                //todo attack other player
                AttackPlayer(player, step.value);
            }
            //OnPlayerData?.Invoke(player); 
            hud.HandlePlayerDataChange(player);
        }
        public void CollectSteps(Player player, Step step)
        {
            if (step.type == Step.StepType.Collect)
            {
                if (((Collect)step).collectType == Step.CollectType.Gold)
                {
                    player.playerData.Golds += step.value;
                    if (AudioManager.instance) AudioManager.instance.PlaySFX(coin);
                }
                else if (((Collect)step).collectType == Step.CollectType.Potion)
                {
                    player.playerData.potions += step.value;
                    if (AudioManager.instance) AudioManager.instance.PlaySFX(coin);
                }
                else if (((Collect)step).collectType == Step.CollectType.Bomb)
                {
                    player.playerData.bombs += step.value;
                    if (AudioManager.instance) AudioManager.instance.PlaySFX(coin);
                }
            }
            else if (step.type == Step.StepType.Skip)
            {
                player.playerData.skipTurns += step.value;
            }
            
            //OnPlayerData?.Invoke(player);
            hud.HandlePlayerDataChange(player);
        }
        public void AttackPlayer(Player player, int value)
        {
            ToggleHUDRaycast(false);
            mouseHover.GetComponent<SpriteRenderer>().sprite = sprites[1];
            mouseHover.gameObject.SetActive(true);
            player.canInteract = true; //todo not all interact can interact
            eventSystem.SetActive(true);
            player.ChosingPlayerToAttack(value);
        }
        public void DoneAttack()
        {
            ToggleHUDRaycast(true);
            mouseHover.gameObject.SetActive(false);
            players[currentPlayer - 1].canInteract = false;
            eventSystem.SetActive(false);
        }
        public void ItemUsing(int id, bool activate)
        {
            //eventSystem.SetActive(!activate);
            ToggleHUDRaycast(!activate);
            if (id == 0)
            {
                mouseHover.GetComponent<SpriteRenderer>().sprite = sprites[8];
                mouseHover.gameObject.SetActive(activate);
            }
            else if (id == 1)
            {
                mouseHover.GetComponent<SpriteRenderer>().sprite = sprites[7];
                mouseHover.gameObject.SetActive(activate);
            }
        }
        #endregion
        [HideInInspector] public bool previousES = false;
        public void TogglePause()
        {
            if (gameState == GameState.Playing) // pause the game
            {
                previousES = eventSystem.activeInHierarchy;
                eventSystem.SetActive(true);
                gameState = GameState.Paused;
                AudioManager.instance.PauseMusic();
                PauseMenu.instance.TurnOn(null);
                Time.timeScale = 0;
            }
            else // unpaused
            {
                eventSystem.SetActive(previousES);
                gameState = GameState.Playing;
                AudioManager.instance.ResumeMusic();
                PauseMenu.instance.TurnOff(false);
                Time.timeScale = 1;
            }
        }
    }
}
