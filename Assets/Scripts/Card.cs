using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardEffects {
    DrawCard,
    DiscardCard,
    GainMoney,
    LoseMoney,
    GainHealth,
    LoseHealth,
    GainMoneyByWageHours,
    LoseMoneyByWageHours,
    GainWage,
    LoseWage,
}

[System.Serializable]
public class CardEffect {
    public CardEffects effect = CardEffects.DrawCard;
    public int amount = 0;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int costMoney = 0;
    public int costHealth = 0;
    public string description;
    public bool isGoodCard = true;
    public List<CardEffect> effects;
}
