using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : CardBase
{
    // Card card on CardBase

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            PlayerCardManager.instance.PlayCard(card);
        }

        // TODO card zoom
        // if (Input.GetMouseButtonDown(1)) {
        //     Debug.Log(nameText + " right clicked");
        // }
    }
}
