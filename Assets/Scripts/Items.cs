using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public static int itemID = 0;
    public static bool onActive = false;
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
                    GameManager.Instance.ItemUsing(id, onActive);
                    return;
                }
                break;

            case 1:
                if (GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].playerData.potions <= 0)
                {
                    onActive = false;
                    GameManager.Instance.ItemUsing(id, onActive);
                    return;
                }
                break;
        }
        if (itemID != id)
        {
            onActive = true;
        }
        else
        {
            onActive = !onActive;
        }
        itemID = id;
        GameManager.Instance.ItemUsing(id,onActive);
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
}
