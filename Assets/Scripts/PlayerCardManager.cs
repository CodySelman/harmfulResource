using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCardManager : MonoBehaviour
{
    // singleton instance
    public static PlayerCardManager instance = null;

    [SerializeField]
    private GameObject playerCard;
    public TMP_Text deckNumCards;
    public TMP_Text discardNumCards;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    private int startingHandSize = 5;
    [HideInInspector]
    public CardManager cardManager;

    // TODO delete me
    public List<GameObject> handCardPositions;
    [SerializeField]
    private GameObject handCardParent;
    [SerializeField]
    private int handMinXPos = -5;
    [SerializeField]
    private int handMaxXPos = 5;

    void Awake() {
        // singleton setup
        if (instance == null) {
            Object.DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        // GameController handles initialization
    }

    public void Initialize() {
        deck = CardUtility.Shuffle(GameController.instance.testDeck);
        discardPile = new List<Card>();
        hand = new List<Card>();
        cardManager = new CardManager(deck, discardPile, hand, startingHandSize);
        UpdateAllDisplay();
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

    // TODO consolidate with SupplyCardManager method?
    void UpdateHandDisplay() {
        // remove old hand render
        foreach (GameObject pos in handCardPositions) {
            foreach (Transform child in pos.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        // render cards in hand
        for (int i = 0; i < hand.Count; i++) {
            // if (i >=6) break;
            // determine card positions based on number of cards
            GameObject handCard = Object.Instantiate(playerCard);
            handCard.GetComponent<CardBase>().card = hand[i];
            handCard.transform.parent = handCardPositions[i].transform;
            handCard.transform.localPosition = Vector3.zero;
        }

        
    }

    public void UpdateAllDisplay() {
        UpdateDeckNumCardsText();
        UpdateDiscardNumCardsText();
        UpdateHandDisplay();
    }

    public void PlayCard(Card card) {
        foreach (CardEffect cardEffect in card.effects) {
            switch (cardEffect.effect) {
                case CardEffects.DrawCard:
                    cardManager.DrawCards(cardEffect.amount);
                    break;
                case CardEffects.DiscardCard:
                    cardManager.DiscardCards(cardEffect.amount);
                    break;
                case CardEffects.GainMoney:
                    GameController.instance.Money += cardEffect.amount;
                    break;
                case CardEffects.LoseMoney:
                    GameController.instance.Money -= cardEffect.amount;
                    break;
                case CardEffects.GainHealth:
                    GameController.instance.MentalHealth += cardEffect.amount;
                    break;
                case CardEffects.LoseHealth:
                    GameController.instance.MentalHealth -= cardEffect.amount;
                    break;
                default:
                    Debug.Log("PlayCard default hit for card: " + card.name);
                    break;
            }
        }
        cardManager.DiscardCard(card);
        UpdateAllDisplay();
    }

    public void DiscardHand() {
        cardManager.DiscardHand();
        UpdateAllDisplay();
    }

    public void DrawHand() {
        cardManager.DiscardHand();
        cardManager.DrawHand();
        UpdateAllDisplay();
    }


}
