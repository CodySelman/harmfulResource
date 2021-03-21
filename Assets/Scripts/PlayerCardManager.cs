using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    [HideInInspector]
    public int startingHandSize = 5;

    // public List<Card> GetDeck() {
    //     return deck;
    // }

    // private void SetDeck(List<Card> cards) {
    //     deck = cards;
    // }

    public List<Card> GetDiscardPile() {
        return discardPile;
    }

    public List<Card> GetHand() {
        return hand;
    }

    public void AddCardToDiscard(Card card) {
        discardPile.Insert(0, card);
    }

    // unsafe because it doesn't check whether there is a card to draw
    private void UnsafeDrawCard() {
        // remove top card from deck and add it to hand
        Card temp = deck[0];
        deck.RemoveAt(0);
        hand.Add(temp);
    }

    // draws a card from deck to hand
    // returns true if a card was drawn, false if there is no card to draw
    public bool DrawCard() {
        // if deck has at least one card, draw it
        if (deck.Count >= 1) {
            UnsafeDrawCard();
        } else if (discardPile.Count >= 1) {
            // if discardPile has at least one card, shuffle discard into deck then draw
            deck = CardUtility.Shuffle(discardPile);
            discardPile.RemoveAll((c) => true);
            UnsafeDrawCard();
        } else {
            return false;
        }
        return true;
    }

    // draws cards equal to numCardsToDraw
    // returns true if able to successfully draw that many cards
    // returns false if unable to draw that many cards
    public bool DrawCards(int numCardsToDraw) {
        bool success = false;
        for (int i = 1; i <= numCardsToDraw; i++) {
            success = DrawCard();
            if (!success) break;
        }
        return success;
    }

    public void DrawCardTest() {
        DrawCard();
    }

    public void DrawCardsTest(int a) {
        DrawCards(a);
    }

    public void DrawHand() {
        DrawCards(startingHandSize);
    }

    // discards a card from hand to discardPile
    // returns true if a card was discarded, false if there is no cards in hand
    public bool DiscardFirstCard() {
        if (hand.Count >= 1) {
            Card temp = hand[0];
            hand.RemoveAt(0);
            discardPile.Add(temp);
            return true;
        }
        return false;
    }

    public bool DiscardCards(int numCardsToDiscard) {
        bool success = false;
        for (int i = 1; i <= numCardsToDiscard; i++) {
            success = DiscardFirstCard();
            if (!success) break;
        }
        return success;
    }

    public void DiscardFirstCardTest() {
        DiscardFirstCard();
    }

    public void DiscardCardsTest(int a) {
        DiscardCards(a);
    }

    public void DiscardHand() {
        DiscardCards(hand.Count);
    }

    public void LogDeck() {
        Debug.Log("Deck:");
        for (int i = 0; i < deck.Count; i++) {
            Debug.Log("- " + i + ": " + deck[i].name);
        }
    }

    public void LogHand() {
        Debug.Log("Hand:");
        for (int i = 0; i < hand.Count; i++) {
            Debug.Log("- " + i + ": " + hand[i].name);
        }
    }

    public void LogDiscard() {
        Debug.Log("Discard:");
        for (int i = 0; i < discardPile.Count; i++) {
            Debug.Log("- " + i + ": " + discardPile[i].name);
        }
    }
}
