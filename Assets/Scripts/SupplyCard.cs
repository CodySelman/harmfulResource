using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCard : CardBase
{
    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            SupplyCardManager.instance.BuyCard(card);
        }

        // TODO card zoom
        // if (Input.GetMouseButtonDown(1)) {
        //     Debug.Log(nameText + " right clicked");
        // }
    }
}
