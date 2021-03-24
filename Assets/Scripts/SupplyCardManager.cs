using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    private CardManager cardManager;
    private int startingHandSize = 5;

    void Start() {
        cardManager = new CardManager(deck, discardPile, hand, startingHandSize);
    }
}
