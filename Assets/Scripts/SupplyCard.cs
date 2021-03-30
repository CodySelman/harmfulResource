using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SupplyCard : CardBase
{
    private bool isHover = false;
    private Vector3 origPos;

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

        origPos = transform.position;
    }

    void Update() {
        if (!GameController.instance.CheckForBusy()) {
            // animation would be better here but this is fine now
            if (isHover) {
                // lerp towards hover position and scale
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -50);
                transform.position = newPos;
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
            } else if (transform.position != origPos) {
                transform.position = origPos;
                transform.localScale = Vector3.one;
            }
        }
    }

    void OnMouseOver() {
        if (!GameController.instance.CheckForBusy()) {
            if (Input.GetMouseButtonDown(0)) {
                SupplyCardManager.instance.BuyCard(card);
            }

            // TODO card zoom
            // if (Input.GetMouseButtonDown(1)) {
            //     Debug.Log(nameText + " right clicked");
            // }
        }
    }

    void OnMouseEnter() {
        if (!GameController.instance.CheckForBusy()) {
            AudioManager.instance.OnCardHover();
            isHover = true;
        }
    }

    void OnMouseExit() {
        isHover = false;
    }
}
