using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsSound : MonoBehaviour, IPointerClickHandler
{
    public AudioClip Sound = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Sound)
        {
            AudioManager.instance.PlaySFX(Sound);
        }
    }
}
