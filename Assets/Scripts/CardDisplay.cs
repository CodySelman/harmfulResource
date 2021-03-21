using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public SpriteRenderer art;

    void Start() {
        nameText.text = card.name;
        descriptionText.text = card.description;
        art.sprite = card.image;
    }
}
