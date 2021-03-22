using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject card;
    public TMP_Text deckNumCards;
    public TMP_Text discardNumCards;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    private int startingHandSize = 5;
    private CardManager cardManager;

    public List<GameObject> handCardPositions;

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

    void UpdateHandDisplay() {
        // remove old hand render
        foreach (GameObject pos in handCardPositions) {
            foreach (Transform child in pos.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        // render cards in hand
        for (int i = 0; i < hand.Count; i++) {
            if (i >=6) break;
            GameObject handCard = Object.Instantiate(card);
            handCard.GetComponent<CardDisplay>().card = hand[i];
            handCard.transform.parent = handCardPositions[i].transform;
            handCard.transform.localPosition = Vector3.zero;
        }
    }

    void UpdateAllDisplay() {
        UpdateDeckNumCardsText();
        UpdateDiscardNumCardsText();
        UpdateHandDisplay();
    }

    // TODO delete me
    public void DrawCardTest() {
        cardManager.DrawCard();
        UpdateAllDisplay();
    }

    // TODO delete me
    public void DrawCardsTest(int a) {
        cardManager.DrawCards(a);
        UpdateAllDisplay();
    }

    // TODO delete me
    public void DiscardFirstCardTest() {
        cardManager.DiscardFirstCard();
        UpdateAllDisplay();
    }

    // TODO delete me
    public void DiscardCardsTest(int a) {
        cardManager.DiscardCards(a);
        UpdateAllDisplay();
    }

    // TODO delete me
    public void DiscardHandTest() {
        cardManager.DiscardHand();
        UpdateAllDisplay();
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
