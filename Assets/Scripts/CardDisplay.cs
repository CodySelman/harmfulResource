using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    private Card card;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public SpriteRenderer art;

    void Start() {
        CardBase cardBase = transform.GetComponent<CardBase>();
        if (cardBase != null) {
            card = transform.GetComponent<CardBase>().card;
            nameText.text = card.name;
            descriptionText.text = card.description;
            art.sprite = card.image;
        } else {
            Debug.Log("CardBase not found");
        }
    }
}
