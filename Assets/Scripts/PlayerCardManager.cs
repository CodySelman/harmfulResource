using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCardManager : MonoBehaviour
{
    public TMP_Text deckNumCards;
    public TMP_Text discardNumCards;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    [HideInInspector]
    private int startingHandSize = 5;

    private CardManager cardManager;

    void Start() {
        cardManager = new CardManager(deck, discardPile, hand, startingHandSize);
        UpdateDeckNumCardsText();
        UpdateDiscardNumCardsText();
    }

    void UpdateDeckNumCardsText() {
        deckNumCards.text = deck.Count.ToString();
    }

    void UpdateDiscardNumCardsText() {
        discardNumCards.text = discardPile.Count.ToString();
    }

    void UpdateDeckAndDiscardNumCardsText() {
        deckNumCards.text = deck.Count.ToString();
        discardNumCards.text = discardPile.Count.ToString();
    }

    // TODO delete me
    public void DrawCardTest() {
        cardManager.DrawCard();
        UpdateDeckAndDiscardNumCardsText();
    }

    // TODO delete me
    public void DrawCardsTest(int a) {
        cardManager.DrawCards(a);
        UpdateDeckAndDiscardNumCardsText();
    }

    // TODO delete me
    public void DiscardFirstCardTest() {
        cardManager.DiscardFirstCard();
        UpdateDiscardNumCardsText();
    }

    // TODO delete me
    public void DiscardCardsTest(int a) {
        cardManager.DiscardCards(a);
        UpdateDiscardNumCardsText();
    }

    // TODO delete me
    public void DiscardHandTest() {
        cardManager.DiscardHand();
        UpdateDiscardNumCardsText();
    }

    // TODO delete me
    public void LogDeck() {
        Debug.Log("Deck:");
        for (int i = 0; i < deck.Count; i++) {
            Debug.Log("- " + i + ": " + deck[i].name);
        }
    }

    // TODO delete me
    public void LogHand() {
        Debug.Log("Hand:");
        for (int i = 0; i < hand.Count; i++) {
            Debug.Log("- " + i + ": " + hand[i].name);
        }
    }

    // TODO delete me
    public void LogDiscard() {
        Debug.Log("Discard:");
        for (int i = 0; i < discardPile.Count; i++) {
            Debug.Log("- " + i + ": " + discardPile[i].name);
        }
    }
}
