using Assets.Scripts;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerUI : MonoBehaviour
{
    [SerializeField] Button _rollButton;
    [SerializeField] TMP_Text _resultsText;
    [SerializeField] DiceRoller2D _diceRoller;
    void Start()
    {
        GameManager.Instance.OnRollDices += RollDice;
        _rollButton.onClick.AddListener(RollDice);
        _diceRoller.OnRoll += HandleRoll;
        gameObject.SetActive(false);
    }
    void HandleRoll(int obj)
    {
        _resultsText.text = $"You rolled a {obj}";
        StartCoroutine(UnDisplay(obj));
    }
    IEnumerator UnDisplay(int obj)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        GameManager.Instance.HandleRoll(obj);
    }
    void RollDice()
    {
        GameManager.Instance.OnRollDice();
        ClearResults();
        _diceRoller.RollDice();
    }

    void ClearResults()
    {
        _resultsText.text = "";
    }
}
