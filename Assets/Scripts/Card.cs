using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffects {
    DrawCard,
    DiscardCard,
}

[System.Serializable]
public class CardEffect {
    public CardEffects effect;
    public int amount;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;
    public List<CardEffect> effects;
}
