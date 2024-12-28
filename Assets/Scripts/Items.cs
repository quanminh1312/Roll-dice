using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Items : MonoBehaviour, IDeselectHandler
{
    public static int itemID = 0;
    public static bool onActive = false;
    public static bool prevActive = false;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnClick(int id)
    {
        switch(id)
        {
            case 0:
                if (GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].playerData.bombs <= 0)
                {
                    onActive = false;
                    GameManager.Instance.ItemUsing(id, onActive); EventSystem.current.SetSelectedGameObject(null);
                    return;
                }
                break;

            case 1:
                if (GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].playerData.potions <= 0)
                {
                    onActive = false;
                    GameManager.Instance.ItemUsing(id, onActive); EventSystem.current.SetSelectedGameObject(null);
                    return;
                }
                break;
        }
        if (itemID != id)
        {
            if (onActive) prevActive = true;
            else prevActive = false;
            onActive = true;
        }
        else
        {
            onActive = !onActive;
        }
        itemID = id;
        GameManager.Instance.ItemUsing(id,onActive);
        if (!onActive) EventSystem.current.SetSelectedGameObject(null);
    }
    public void OnMouseHover()
    {
        if (image.material.HasProperty("_OutlineEnabled"))
        {
            image.material.SetFloat("_OutlineEnabled", 1);
        }
    }
    public void OffMouseHover()
    {
        if (image.material.HasProperty("_OutlineEnabled"))
        {
            image.material.SetFloat("_OutlineEnabled", 0);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(Deselect());
        prevActive = false;
    }
    IEnumerator Deselect()
    {
        yield return new WaitForSeconds(0.1f);
        if (prevActive) yield break;
        onActive = false;
        GameManager.Instance.ItemUsing(itemID, onActive);
    }
}
