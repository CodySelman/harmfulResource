using System.Collections;
using System.Collections.Generic;

public class CardManager
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();
    public int handSize;

    public CardManager(List<Card> _deck, List<Card> _discardPile, List<Card> _hand, int _handSize) {
        deck = _deck;
        discardPile = _discardPile;
        hand = _hand;
        handSize = _handSize;
    }

    #region Draw Card Methods
    // unsafe because it doesn't check whether there is a card to draw
    private void UnsafeDrawCard() {
        // remove top card from deck and add it to hand
        Card temp = deck[0];
        deck.RemoveAt(0);
        hand.Add(temp);
    }

    ///<summary>
    ///Draws a card from deck to hand
    ///</summary>
    public void DrawCard() {
        // if deck has at least one card, draw it
        if (deck.Count >= 1) {
            UnsafeDrawCard();
        } else if (discardPile.Count >= 1) {
            // if discardPile has at least one card, shuffle discard into deck then draw
            deck = CardUtility.Shuffle(discardPile);
            discardPile.RemoveAll((c) => true);
            UnsafeDrawCard();
        }
    }

    ///<summary>
    ///Draws num card from deck to hand
    ///</summary>
    public void DrawCards(int num) {
        for (int i = 1; i <= num; i++) {
            DrawCard();
        }
    }

    ///<summary>
    ///Draws {handSize} cards from deck to hand
    ///</summary>
    public void DrawHand() {
        DrawCards(handSize);
    }
    #endregion

    #region Discard Card Methods
    ///<summary>
    ///Moves card from hand[0] to discard
    ///</summary>
    public void DiscardFirstCard() {
        if (hand.Count >= 1) {
            Card temp = hand[0];
            hand.RemoveAt(0);
            discardPile.Add(temp);
        }
    }

    ///<summary>
    ///Discards {num} cards from hand to discard
    ///</summary>
    public void DiscardCards(int num) {
        for (int i = 1; i <= num; i++) {
            DiscardFirstCard();
        }
    }

    ///<summary>
    ///Discards all cards from hand to discard
    ///</summary>
    public void DiscardHand() {
        DiscardCards(hand.Count);
    }

    ///<summary>
    ///Adds specified card to discard
    ///</summary>
    public void AddCardToDiscard(Card card) {
        discardPile.Insert(0, card);
    }
    #endregion

    #region Trash Card Methods
    ///<summary>
    ///Trashes specified card from specified location
    ///</summary>
    public void TrashCard(List<Card> cardLocation, Card cardToTrash) {
        if (cardLocation.Contains(cardToTrash)) {
            cardLocation.Remove(cardToTrash);
        }
    }
    #endregion
}
