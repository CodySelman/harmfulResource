using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SupplyCard : CardBase
{
    public TMP_Text costMoney;
    public TMP_Text costHealth;

    void Start() {
        if (card.costMoney > 0) {
            costMoney.text = "$" + card.costMoney.ToString();
        } else {
            costMoney.text = "";
        }
        if (card.costHealth > 0) {
            costHealth.text = card.costHealth.ToString();
        } else {
            costHealth.text = "";
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            SupplyCardManager.instance.BuyCard(card);
        }

        // TODO card zoom
        // if (Input.GetMouseButtonDown(1)) {
        //     Debug.Log(nameText + " right clicked");
        // }
    }

    void OnMouseEnter() {
        AudioManager.instance.OnCardHover();
    }
}
