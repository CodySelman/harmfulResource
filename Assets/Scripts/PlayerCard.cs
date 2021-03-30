using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCard : CardBase
{
    // Card card on CardBase
    private bool isHover = false;
    private Vector3 origPos;

    [SerializeField]
    private float hoverEnterSpeed = 5.0f;
    [SerializeField]
    private float hoverExitSpeed = 10.0f;
    [SerializeField]
    private float hoverYPos = -3;
    [SerializeField]
    private float playYPos = 1.5f;
    [SerializeField]
    private float playZPos = -10f;
    [SerializeField]
    private Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1);

    private bool isBeingPlayed = false;
    private bool hasPlayed = false;

    public GameObject effectHealthGO;
    public GameObject effectHealthSymbol;
    public TMP_Text effectHealth;
    public GameObject effectMoneyGO;
    public TMP_Text effectMoney;
    public GameObject effectWageGO;
    public TMP_Text effectWage;
    public GameObject effectCardGO;
    public TMP_Text effectCard;

    void Start() {
        origPos = transform.position;

        foreach (CardEffect effect in card.effects) {
            if (effect.effect == CardEffects.GainHealth || effect.effect == CardEffects.LoseHealth) {
                effectHealthGO.SetActive(true);
                if (effect.effect == CardEffects.GainHealth) {
                    effectHealth.text = "+" + effect.amount;
                } else {
                    effectHealth.text = "-" + effect.amount;
                }
            }
        }
        foreach (CardEffect effect in card.effects) {
            if (effect.effect == CardEffects.GainMoney || effect.effect == CardEffects.LoseMoney) {
                effectMoneyGO.SetActive(true);
                if (effect.effect == CardEffects.GainMoney) {
                    effectMoney.text = "+" + effect.amount;
                } else {
                    effectMoney.text = "-" + effect.amount;
                }
            }
        }
        foreach (CardEffect effect in card.effects) {
            if (effect.effect == CardEffects.GainWage || effect.effect == CardEffects.LoseWage) {
                effectWageGO.SetActive(true);
                if (effect.effect == CardEffects.GainWage) {
                    effectWage.text = "+" + effect.amount;
                } else {
                    effectWage.text = "-" + effect.amount;
                }
            }
        }
        foreach (CardEffect effect in card.effects) {
            if (effect.effect == CardEffects.DrawCard || effect.effect == CardEffects.DiscardCard) {
                effectCardGO.SetActive(true);
                if (effect.effect == CardEffects.DrawCard) {
                    effectCard.text = "+" + effect.amount;
                } else {
                    effectCard.text = "-" + effect.amount;
                }
            }
        }
    }

    void Update() {
        if (!GameController.instance.CheckForBusy() && !isBeingPlayed) {
            // animation would be better here but this is fine now
            if (isHover) {
                // lerp towards hover position and scale
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -50);
                transform.position = newPos;
                transform.localScale = hoverScale;

            } else if (transform.position != origPos) {
                transform.position = origPos;
                transform.localScale = Vector3.one;
            }
        }
        if (isBeingPlayed) {
            transform.localScale = hoverScale;
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(transform.position.x, playYPos, playZPos),
                10 * Time.deltaTime
            );
            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, playYPos, playZPos)) <= 0.1f && !hasPlayed) {
                PlayerCardManager.instance.PlayCard(card);
                hasPlayed = true;
            }
        }
    }

    void OnMouseOver() {
        if (!GameController.instance.CheckForBusy()) {
            if (Input.GetMouseButtonDown(0)) {
                isBeingPlayed = true;
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
